namespace Works.Application.Interfaces
{
    public interface IFileStorageClient
    {
        Task<Stream> DownloadAsync(Guid fileId);
        Task<Guid> UploadAsync(Stream content, string fileName, string contentType);
    }

}