using Analysis.Application.Interfaces;
using Analysis.Infrastructure.Data;
using Analysis.Infrastructure.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
                string? connectionString = builder.Configuration.GetConnectionString("FileStorageDatabase");
                options.UseSqlite(connectionString);
            });
            
            builder.Services.AddScoped<IRepository<WorkAnalysis>>(sp =>
            {
                AnalysisDbContext dbContext = sp.GetRequiredService<AnalysisDbContext>();
                EfRepository<WorkAnalysis> efRepo = new(dbContext);
                return efRepo;
            });

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
            
            using (IServiceScope scope = app.Services.CreateScope())
            { 
                AnalysisDbContext db = scope.ServiceProvider.GetRequiredService<AnalysisDbContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}