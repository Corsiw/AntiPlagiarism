namespace Works.Application.UseCases.AttachFile
{
    public record AttachFileRequest(
        Stream FileStream,
        string FileName,
        string ContentType
    );
}