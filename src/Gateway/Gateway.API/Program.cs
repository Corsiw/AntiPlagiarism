namespace Gateway.API
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
            builder.Services.AddSwaggerGen();
            
            // YARP ReverseProxy
            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                // Use multiple endpoints for all microservices
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/works/swagger/v1/swagger.json", "Works API v1");
                    c.SwaggerEndpoint("/files/swagger/v1/swagger.json", "Files API v1");
                    c.SwaggerEndpoint("/analysis/swagger/v1/swagger.json", "Analysis API v1");
                });
            }

            app.UseAuthorization();
            
            app.MapReverseProxy();
            
            app.Run();
        }
    }
}