using Domain.Entities;

namespace Analysis.Application.Interfaces
{
    public interface IAnalyzeProvider
    {
        Task<Report> AnalyzeAsync(AnalysisRecord analysisRecord);
    }
}