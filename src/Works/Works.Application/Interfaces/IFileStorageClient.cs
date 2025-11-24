namespace Works.Application.Interfaces
{
    public interface IFileStorageClient
    {
        Task<Stream> DownloadAsync(string fileId);
        Task<string> UploadAsync(Stream content, string fileName, string contentType);
    }

}