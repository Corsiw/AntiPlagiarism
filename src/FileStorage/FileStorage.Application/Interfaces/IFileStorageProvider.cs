namespace FileStorage.Application.Interfaces
{
    public interface IFileStorageProvider
    {
        Task<string> SaveAsync(Stream stream, string fileName, string contentType);
        Task<Stream> GetAsync(string storagePath);
        Task DeleteAsync(string storagePath);
    }
}