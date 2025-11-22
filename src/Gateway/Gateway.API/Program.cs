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

            string[] summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
                {
                    var forecast = Enumerable.Range(1, 5).Select(index =>
                            new WeatherForecast
                            {
                                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                                TemperatureC = Random.Shared.Next(-20, 55),
                                Summary = summaries[Random.Shared.Next(summaries.Length)]
                            })
                        .ToArray();
                    return forecast;
                })
                .WithName("GetWeatherForecast")
                .WithOpenApi();
            
            app.MapReverseProxy();
            
            app.Run();
        }
    }
}