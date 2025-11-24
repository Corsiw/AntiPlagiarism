using Works.Application.Interfaces;

namespace Infrastructure.Clients
{
    public class FileStorageClient(HttpClient client) : IFileStorageClient
    {
        public async Task<Stream> DownloadAsync(string fileId)
        {
            HttpResponseMessage response =
                await client.GetAsync($"/{fileId}", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStreamAsync();
        }

        public async Task<string> UploadAsync(Stream content, string fileName, string contentType)
        {
            MultipartFormDataContent form = new()
            {
                { new StreamContent(content), fileName, $"{fileName}.bin" }
            };

            HttpResponseMessage response = await client.PostAsync("/upload", form);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}