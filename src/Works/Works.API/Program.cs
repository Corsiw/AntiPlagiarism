using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Works API", Version = "v1" });

                // Указываем basePath, который будет отображаться в Swagger UI
                c.AddServer(new OpenApiServer
                {
                    Url = "/works", // <-- здесь префикс Gateway
                    Description = "Access via API Gateway"
                });
            });
            
            // Configure Sqlite in appsettings.json
            builder.Services.AddDbContext<WorksDbContext>(options =>
            {
                string? connectionString = builder.Configuration.GetConnectionString("WorksDatabase");
                options.UseSqlite(connectionString);
            });

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
        
            app.UseAuthorization();
            
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