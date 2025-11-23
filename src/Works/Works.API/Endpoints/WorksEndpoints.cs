using Works.Application.UseCases.AddWork;
using Works.Application.UseCases.AnalyzeWork;
using Works.Application.UseCases.AttachFile;
using Works.Application.UseCases.GetReport;
using Works.Application.UseCases.GetWorkById;
using Works.Application.UseCases.ListWorks;

namespace Works.API.Endpoints
{
    public static class WorksEndpoints
    {
        public static WebApplication MapWorksEndpoints(this WebApplication app)
        {
            app.MapGroup("/")
                .WithTags("Works")
                .MapGetWorks()
                .MapGetWorkById()
                .MapAddWork()
                .MapAttachFile()
                .MapAnalyzeWork()
                .MapGetReport();

            return app;
        }

        private static RouteGroupBuilder MapGetWorks(this RouteGroupBuilder group)
        {
            group.MapGet("", async (IListWorksRequestHandler handler) =>
                {
                    ListWorksResponse response = await handler.HandleAsync();
                    return Results.Ok(response);
                })
                .WithName("GetWorks")
                .WithSummary("Get all works")
                .WithDescription("Get all works from the database")
                .WithOpenApi();
            return group;
        }

        private static RouteGroupBuilder MapGetWorkById(this RouteGroupBuilder group)
        {
            group.MapGet("{workId:guid}", async (Guid workId, IGetWorkByIdRequestHandler handler) =>
                {
                    GetWorkByIdResponse? response = await handler.HandleAsync(workId);
                    return response is not null ? Results.Ok(response) : Results.NotFound();
                })
                .WithName("GetWorkById")
                .WithSummary("Get work by ID")
                .WithDescription("Get detailed information about a specific work")
                .WithOpenApi();
            return group;
        }

        private static RouteGroupBuilder MapAddWork(this RouteGroupBuilder group)
        {
            group.MapPost("", async (AddWorkRequest request, IAddWorkRequestHandler handler) =>
                {
                    AddWorkResponse response = await handler.HandleAsync(request);
                    return Results.Created($"/works/{response.WorkId}", response);
                })
                .WithName("AddWork")
                .WithSummary("Add a new work")
                .WithDescription("Create a new work submission record")
                .WithOpenApi();
            return group;
        }

        private static RouteGroupBuilder MapAttachFile(this RouteGroupBuilder group)
        {
            group.MapPost("{workId:guid}/file", async (Guid workId, AttachFileRequest request, IAttachFileRequestHandler handler) =>
                {
                    AttachFileResponse response = await handler.HandleAsync(workId, request);
                    return Results.Ok(response);
                })
                .WithName("AttachFile")
                .WithSummary("Attach file to work")
                .WithDescription("Upload a file and attach it to an existing work")
                .WithOpenApi();
            return group;
        }

        private static RouteGroupBuilder MapAnalyzeWork(this RouteGroupBuilder group)
        {
            group.MapPost("{workId:guid}/analyze", async (Guid workId, IAnalyzeWorkRequestHandler handler) =>
                {
                    AnalyzeWorkResponse response = await  handler.HandleAsync(workId);
                    return Results.Ok(response);
                })
                .WithName("AnalyzeWork")
                .WithSummary("Analyze work for plagiarism")
                .WithDescription("Trigger analysis for the specified work")
                .WithOpenApi();
            return group;
        }

        private static RouteGroupBuilder MapGetReport(this RouteGroupBuilder group)
        {
            group.MapGet("{workId:guid}/report", async (Guid workId, IGetReportRequestHandler handler) =>
                {
                    GetReportResponse? response = await handler.HandleAsync(workId);
                    return response is not null ? Results.Ok(response) : Results.NotFound();
                })
                .WithName("GetReport")
                .WithSummary("Get analysis report for work")
                .WithDescription("Get the plagiarism report for a specific work")
                .WithOpenApi();
            return group;
        }
    }
}
