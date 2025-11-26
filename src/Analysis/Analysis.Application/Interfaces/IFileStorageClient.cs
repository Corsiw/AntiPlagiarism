namespace Analysis.Application.Interfaces
{
    public interface IFileStorageClient
    {
        Task<Stream> DownloadAsync(string fileId);
        Task<Guid> UploadAsync(Stream content, string fileName, string contentType);
    }

}