using FileStorage.Application.Interfaces;

namespace FileStorage.Infrastructure.StorageProviders
{
    public class LocalFileStorageProvider(string basePath) : IFileStorageProvider
    {
        public async Task<string> SaveAsync(Stream stream, string fileName, string contentType)
        {
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            await using FileStream fs = File.Create(filePath);
            await stream.CopyToAsync(fs);
            return filePath;
        }

        public Task<Stream> GetAsync(string storagePath)
        {
            Stream fs = File.OpenRead(storagePath);
            return Task.FromResult(fs);
        }

        public Task DeleteAsync(string storagePath)
        {
            File.Delete(storagePath);
            return Task.CompletedTask;
        }
    }

}