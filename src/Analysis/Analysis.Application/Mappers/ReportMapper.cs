using Analysis.Application.UseCases.GetReportById;
using Domain.Entities;

namespace Analysis.Application.Mappers
{
    public class ReportMapper : IReportMapper
    {
        public GetReportByIdResponse MapEntityToGetReportByIdResponse(Report report)
        {
            return new GetReportByIdResponse(
                report.ReportId,
                report.AnalysisRecordId,
                report.FileId,
                report.IsPlagiarism,
                report.SimilarityPercentage
            );
        }
    }
}