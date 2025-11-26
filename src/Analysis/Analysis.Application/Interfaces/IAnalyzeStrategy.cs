using Domain.Entities;

namespace Analysis.Application.Interfaces
{
    public interface IAnalyzeStrategy
    {
        Task<AnalysisResult> AnalyzeAsync(Stream fileStream, AnalysisRecord analysisRecord);
    }
}