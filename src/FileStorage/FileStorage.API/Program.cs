using Domain.Entities;
using FileStorage.API.Endpoints;
using FileStorage.Application.Interfaces;
using FileStorage.Application.Mappers;
using FileStorage.Application.UseCases.GetFileById;
using FileStorage.Application.UseCases.UploadFile;
using FileStorage.Infrastructure.Data;
using FileStorage.Infrastructure.Repositories;
using FileStorage.Infrastructure.StorageProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace FileStorage.API
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FileStorage API", Version = "v1" });

                // Указываем basePath, который будет отображаться в Swagger UI
                c.AddServer(new OpenApiServer
                {
                    Url = "/files", // здесь префикс Gateway
                    Description = "Access via API Gateway"
                });
            });
            
            // Configure Sqlite in appsettings.json
            builder.Services.AddDbContext<FileRecordsDbContext>(options =>
            {
                string? connectionString = builder.Configuration.GetConnectionString("FileStorageDatabase");
                options.UseSqlite(connectionString);
            });
            
            builder.Services.AddScoped<IRepository<FileRecord>>(sp =>
            {
                FileRecordsDbContext dbContext = sp.GetRequiredService<FileRecordsDbContext>();
                EfRepository<FileRecord> efRepo = new(dbContext);
                return efRepo;
            });
            
            builder.Services.AddScoped<IFileStorageProvider>(sp =>
            {
                string connectionString = builder.Configuration.GetConnectionString("FileStorageFiles")
                                          ?? throw new InvalidOperationException("FileStorage connection string not found. Check appsettings.json");
                LocalFileStorageProvider provider = new(connectionString);
                return provider;
            });
            
            builder.Services.AddScoped<IUploadFileHandler, UploadFileHandler>();
            builder.Services.AddScoped<IGetFileByIdRequestHandler, GetFileByIdRequestHandler>();
            
            
            builder.Services.AddScoped<IFileRecordMapper, FileRecordMapper>();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FilesStorage.API V1");
                });
            }
            
            app.MapFileStorageEndpoints();

            using (IServiceScope scope = app.Services.CreateScope())
            { 
                FileRecordsDbContext db = scope.ServiceProvider.GetRequiredService<FileRecordsDbContext>();
                db.Database.Migrate();
            }
            
            app.Run();
        }
    }
}