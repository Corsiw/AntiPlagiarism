namespace FileStorage.Application.UseCases.UploadFile
{
    public interface IUploadFileHandler
    {
        Task<UploadFileResponse> HandleAsync(UploadFileRequest request);
    }
}