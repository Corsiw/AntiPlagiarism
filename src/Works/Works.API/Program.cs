using Domain.Entities;
using Infrastructure.Clients;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using Works.API.Endpoints;
using Works.Application.Interfaces;
using Works.Application.Mappers;
using Works.Application.UseCases.AddWork;
using Works.Application.UseCases.AnalyzeWork;
using Works.Application.UseCases.AttachFile;
using Works.Application.UseCases.GetReport;
using Works.Application.UseCases.GetWorkById;
using Works.Application.UseCases.ListWorks;

namespace Works.API
{
    public abstract class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SupportNonNullableReferenceTypes();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Works API", Version = "v1" });

                // Указываем basePath, который будет отображаться в Swagger UI
                c.AddServer(new OpenApiServer
                {
                    Url = "/works", // здесь префикс Gateway
                    Description = "Access via API Gateway"
                });
            });
            
            // Configure Sqlite in appsettings.json
            builder.Services.AddDbContext<WorksDbContext>(options =>
            {
                string? connectionString = builder.Configuration.GetConnectionString("WorksDatabase");
                options.UseSqlite(connectionString);
            });
            
            // Remove AntiForgery cause only API
            // builder.Services.AddAntiforgery(options =>
            // {
            //     options.SuppressXFrameOptionsHeader = true;
            // });
            // builder.Services.AddControllers(options =>
            // {
            //     options.Filters.Remove(new AutoValidateAntiforgeryTokenAttribute());
            // });
            
            // Retry Policy
            builder.Services.AddHttpClient<IFileStorageClient, FileStorageClient>(client =>
                {
                    client.BaseAddress = new Uri("http://filestorage.api:8082");
                    client.Timeout = TimeSpan.FromSeconds(10);
                })
                // Retry for transient errors (HTTP 5xx, network failure)
                .AddPolicyHandler(HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(
                        retryCount: 3,
                        sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(200 * attempt)
                    )
                )
                // Circuit breaker to stop flooding a failing service
                .AddPolicyHandler(HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .CircuitBreakerAsync(
                        handledEventsAllowedBeforeBreaking: 5,
                        durationOfBreak: TimeSpan.FromSeconds(20)
                    )
                )
                // Operation timeout
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(5));

            builder.Services.AddScoped<IRepository<Work>>(sp =>
            {
                WorksDbContext dbContext = sp.GetRequiredService<WorksDbContext>();
                EfRepository<Work> efRepo = new(dbContext);
                return efRepo;
            });
            
            builder.Services.AddScoped<IAddWorkRequestHandler, AddWorkRequestHandler>();
            builder.Services.AddScoped<IListWorksRequestHandler, ListWorksRequestHandler>();
            builder.Services.AddScoped<IGetWorkByIdRequestHandler, GetWorkByIdRequestHandler>();
            builder.Services.AddScoped<IAttachFileRequestHandler, AttachFileRequestHandler>();
            builder.Services.AddScoped<IAnalyzeWorkRequestHandler, AnalyzeWorkRequestHandler>();
            builder.Services.AddScoped<IGetReportRequestHandler, GetReportRequestHandler>();
            
            builder.Services.AddSingleton<IWorkMapper, WorkMapper>();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Works.API V1");
                });
            }
        
            // app.UseAuthorization();
            
            app.MapWorksEndpoints();

            using (IServiceScope scope = app.Services.CreateScope())
            { 
                WorksDbContext db = scope.ServiceProvider.GetRequiredService<WorksDbContext>();
                db.Database.Migrate();   // создаёт works.db и таблицы
            }
            
            app.Run();
        }
    }
}