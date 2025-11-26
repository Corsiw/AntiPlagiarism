using Analysis.Application.UseCases.AnalyzeWork;
using Analysis.Application.UseCases.GetReportById;
using Microsoft.AspNetCore.Mvc;

namespace Analysis.API.Endpoints
{
    public static class AnalysisEndpoints
    {
        public static WebApplication MapAnalysisEndpoints(this WebApplication app)
        {
            app.MapGroup("/")
                .WithTags("Analysis")
                .MapGetReportById()
                .MapAnalyzeWork();

            return app;
        }
        
        private static RouteGroupBuilder MapAnalyzeWork(this RouteGroupBuilder group)
        {
            group.MapPost("", async ([FromBody] AnalyzeWorkRequest request, [FromServices] IAnalyzeWorkHandler handler) =>
                {
                    AnalyzeWorkResponse response = await handler.HandleAsync(request);
                    return Results.Created($"/analyze/{response.ReportId}", response);
                })
                .WithName("AnalyzeWork")
                .WithSummary("Analyze Work")
                .WithDescription("Analyze Work and get Report")
                .WithOpenApi();
            return group;
        } 

        private static RouteGroupBuilder MapGetReportById(this RouteGroupBuilder group)
        {
            group.MapGet("{fileId:guid}", async (Guid fileId, [FromServices] IGetReportByIdRequestHandler handler) =>
                {
                    GetReportByIdResponse? response = await handler.HandleAsync(fileId);
                    return response is not null ? Results.Ok(response) : Results.NotFound();
                })
                .WithName("GetReportById")
                .WithSummary("Get Report By Id")
                .WithDescription("Get a specific report")
                .WithOpenApi();
            return group;
        }
    }
}