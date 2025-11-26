using Analysis.Application.UseCases.AnalyzeWork;
using Analysis.Application.UseCases.GetReportById;
using Domain.Entities;

namespace Analysis.Application.Mappers
{
    public interface IReportMapper
    {
        AnalysisRecord MapAnalyzeWorkRequestToEntity(AnalyzeWorkRequest request);
        AnalyzeWorkResponse MapEntityToAnalyzeWorkResponse(Report entity);
        GetReportByIdResponse MapEntityToGetReportByIdResponse(Report report);
    }
}