namespace Works.Application.UseCases.AttachFile
{
    public record AttachFileResponse
    (
        Guid WorkId,
        string FileId,
        string Status
    );
}