using System.Net.Http.Json;
using System.Text;
using Works.Application.Interfaces;

namespace Infrastructure.Clients
{
    public class WordCloudClient(HttpClient client) : IWordCloudClient
    {
        public async Task<Stream> GetAsync(Stream fileStream)
        {
            string text;
            using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8, true, 8192, leaveOpen: true))
            {
                text = await reader.ReadToEndAsync();
            }

            var chartData = new { text };

            HttpResponseMessage apiResponse = await client.PostAsJsonAsync("wordcloud", chartData);
            apiResponse.EnsureSuccessStatusCode();
            
            return await apiResponse.Content.ReadAsStreamAsync();
        }
    }
}