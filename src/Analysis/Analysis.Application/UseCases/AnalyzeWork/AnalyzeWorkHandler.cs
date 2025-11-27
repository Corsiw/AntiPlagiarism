using Analysis.Application.Interfaces;
using Analysis.Application.Mappers;
using Domain.Entities;

namespace Analysis.Application.UseCases.AnalyzeWork
{
    public class AnalyzeWorkHandler(IRepository<Report> repository, IAnalyzeProvider analyzeProvider, IReportMapper mapper) : IAnalyzeWorkHandler
    {
        public async Task<AnalyzeWorkResponse> HandleAsync(AnalyzeWorkRequest request)
        {
            AnalysisRecord analysisRecord = mapper.MapAnalyzeWorkRequestToEntity(request);
            
            Report report = await analyzeProvider.AnalyzeAsync(analysisRecord);
            await repository.AddAsync(report);
            
            return mapper.MapEntityToAnalyzeWorkResponse(report);
        }
    }
}