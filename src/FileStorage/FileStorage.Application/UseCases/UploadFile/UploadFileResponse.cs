namespace FileStorage.Application.UseCases.UploadFile
{
    public record UploadFileResponse(
        Guid FileId,
        string FileName,
        string Url
    );
}