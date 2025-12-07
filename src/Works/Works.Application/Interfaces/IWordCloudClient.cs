namespace Works.Application.Interfaces
{
    public interface IWordCloudClient
    {
        Task<Stream> GetAsync(Stream stream);
    }
}