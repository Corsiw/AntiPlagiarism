using FileStorage.API.Forms;
using FileStorage.Application.UseCases.GetFileById;
using FileStorage.Application.UseCases.UploadFile;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Endpoints
{
    public static class FileStorageEndpoints
    {
        public static WebApplication MapFileStorageEndpoints(this WebApplication app)
        {
            app.MapGroup("/")
                .WithTags("FileStorage")
                .MapGetFileById()
                .MapPostFile();

            return app;
        }

        private static RouteGroupBuilder MapGetFileById(this RouteGroupBuilder group)
        {
            group.MapGet("{fileId:guid}", async (Guid fileId, [FromServices] IGetFileByIdRequestHandler handler) =>
                {
                    GetFileByIdResponse? response = await handler.HandleAsync(fileId);
                    if (response == null)
                    {
                        return Results.NotFound();
                    }

                    return Results.File(response.FileStream, response.ContentType, response.FileName);
                })
                .WithName("GetFileById")
                .WithSummary("Get File By Id")
                .WithDescription("Get a specific file")
                .WithOpenApi()
                .DisableAntiforgery();
            return group;
        }

        private static RouteGroupBuilder MapPostFile(this RouteGroupBuilder group)
        {
            group.MapPost("upload", async ([FromForm] AttachFileForm form, [FromServices] IUploadFileHandler handler) =>
                {
                    IFormFile file = form.File;
                    UploadFileRequest request = new(file.OpenReadStream(), file.FileName, file.ContentType);
                    UploadFileResponse response = await handler.HandleAsync(request);

                    return Results.Ok(response);
                })
                .WithName("UploadFile")
                .WithSummary("Upload File")
                .WithDescription("Upload File to storage")
                .WithOpenApi()
                .DisableAntiforgery();
            return group;
        }
    }
}