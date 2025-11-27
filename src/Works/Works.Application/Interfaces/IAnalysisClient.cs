using Works.Application.DTO.Analysis;

namespace Works.Application.Interfaces
{
    public interface IAnalysisClient
    {
        Task<AnalyzeWorkResponseDto> AnalyzeWork(AnalyzeWorkRequestDto requestDto);
        Task<GetReportByIdResponseDto> GetReportById(Guid workId);
    }
}