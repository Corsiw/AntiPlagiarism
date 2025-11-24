namespace FileStorage.Application.UseCases.UploadFile
{
    public record UploadFileRequest(
        Stream FileStream,
        string FileName,
        string ContentType);
}