using Analysis.API.Endpoints;
using Analysis.API.Middleware;
using Analysis.Application.Interfaces;
using Analysis.Application.Mappers;
using Analysis.Application.UseCases.AnalyzeWork;
using Analysis.Application.UseCases.GetReportById;
using Analysis.Infrastructure.Analyzers;
using Analysis.Infrastructure.Clients;
using Analysis.Infrastructure.Data;
using Analysis.Infrastructure.Repositories;
using Analysis.Infrastructure.Strategies;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;

namespace Analysis.API
{
    public abstract class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SupportNonNullableReferenceTypes();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Analysis API", Version = "v1" });

                // Указываем basePath, который будет отображаться в Swagger UI
                c.AddServer(new OpenApiServer
                {
                    Url = "/analysis", // здесь префикс Gateway
                    Description = "Access via API Gateway"
                });
            });
            
            // Configure Sqlite in appsettings.json
            builder.Services.AddDbContext<AnalysisDbContext>(options =>
            {
                string? connectionString = builder.Configuration.GetConnectionString("AnalysisDatabase");
                options.UseSqlite(connectionString);
            });
            
            builder.Services.AddScoped<IRepository<AnalysisRecord>>(sp =>
            {
                AnalysisDbContext dbContext = sp.GetRequiredService<AnalysisDbContext>();
                EfRepository<AnalysisRecord> efRepo = new(dbContext);
                return efRepo;
            });
            
            builder.Services.AddScoped<IRepository<Report>>(sp =>
            {
                AnalysisDbContext dbContext = sp.GetRequiredService<AnalysisDbContext>();
                EfRepository<Report> efRepo = new(dbContext);
                return efRepo;
            });
            
            builder.Services.AddScoped<IContentHashRepository>(sp =>
            {
                AnalysisDbContext dbContext = sp.GetRequiredService<AnalysisDbContext>();
                ContentHashRepository cHashRepo = new(dbContext);
                return cHashRepo;
            });

            builder.Services.AddScoped<IAnalyzeStrategy, HashMatchingAnalyzeStrategy>();
            builder.Services.AddScoped<IAnalyzeProvider, AnalyzeProvider>();
            
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

            builder.Services.AddScoped<IGetReportByIdRequestHandler, GetReportByIdRequestHandler>();
            builder.Services.AddScoped<IAnalyzeWorkHandler, AnalyzeWorkHandler>();

            builder.Services.AddScoped<IReportMapper, ReportMapper>();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Analysis.API V1");
                });
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            
            app.MapAnalysisEndpoints();
            
            using (IServiceScope scope = app.Services.CreateScope())
            { 
                AnalysisDbContext db = scope.ServiceProvider.GetRequiredService<AnalysisDbContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}