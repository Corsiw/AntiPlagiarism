using Analysis.Application.Interfaces;
using Analysis.Application.Mappers;
using Domain.Entities;

namespace Analysis.Application.UseCases.AnalyzeWork
{
    public class AnalyzeWorkHandler(IAnalyzeProvider analyzeProvider, IReportMapper mapper) : IAnalyzeWorkHandler
    {
        public async Task<AnalyzeWorkResponse> HandleAsync(AnalyzeWorkRequest request)
        {
            AnalysisRecord analysisRecord = mapper.MapAnalyzeWorkRequestToEntity(request);
            
            Report report = await analyzeProvider.AnalyzeAsync(analysisRecord);
            
            return mapper.MapEntityToAnalyzeWorkResponse(report);
        }
    }
}