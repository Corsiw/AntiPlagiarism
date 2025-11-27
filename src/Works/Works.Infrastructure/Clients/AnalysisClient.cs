using System.Net.Http.Json;
using Works.Application.DTO.Analysis;
using Works.Application.Interfaces;

namespace Infrastructure.Clients
{
    public class AnalysisClient(HttpClient client) : IAnalysisClient
    {
        public async Task<AnalyzeWorkResponseDto> AnalyzeWork(AnalyzeWorkRequestDto requestDto)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/", requestDto);
            response.EnsureSuccessStatusCode();

            AnalyzeWorkResponseDto? json = await response.Content.ReadFromJsonAsync<AnalyzeWorkResponseDto>();
            return json!;
        }

        public async Task<GetReportByIdResponseDto> GetReportById(Guid reportId)
        {
            HttpResponseMessage response =
                await client.GetAsync($"/{reportId}", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            
            GetReportByIdResponseDto? json = await response.Content.ReadFromJsonAsync<GetReportByIdResponseDto>();
            return json!;
        }
    }
}