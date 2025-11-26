namespace Works.Application.UseCases.AttachFile
{
    public record AttachFileResponse
    (
        Guid WorkId,
        Guid FileId,
        string Status
    );
}