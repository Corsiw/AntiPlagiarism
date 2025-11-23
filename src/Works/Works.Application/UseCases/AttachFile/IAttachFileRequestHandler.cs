namespace Works.Application.UseCases.AttachFile
{
    public interface IAttachFileRequestHandler
    {
        Task<AttachFileResponse> HandleAsync(Guid workId, AttachFileRequest request);
    }
}