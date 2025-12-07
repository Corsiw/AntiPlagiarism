using Infrastructure.DTO.FileStorage;
using System.Net.Http.Json;
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

        public async Task<Guid> UploadAsync(Stream stream, string fileName, string contentType)
        {
            using MultipartFormDataContent content = new();
            content.Add(new StreamContent(stream), "file", fileName);

            HttpResponseMessage response = await client.PostAsync("/upload", content);
            response.EnsureSuccessStatusCode();

            UploadFileResponse? json = await response.Content.ReadFromJsonAsync<UploadFileResponse>();
            return json!.FileId;
        }
    }
}