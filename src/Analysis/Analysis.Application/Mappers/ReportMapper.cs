using Analysis.Application.UseCases.AnalyzeWork;
using Analysis.Application.UseCases.GetReportById;
using Domain.Entities;

namespace Analysis.Application.Mappers
{
    public class ReportMapper : IReportMapper
    {
        public AnalysisRecord MapAnalyzeWorkRequestToEntity(AnalyzeWorkRequest request)
        {
            return new AnalysisRecord(
                request.WorkId,
                request.FileId,
                request.StudentId,
                request.AssignmentId,
                request.SubmittedAt
            );
        }

        public AnalyzeWorkResponse MapEntityToAnalyzeWorkResponse(Report entity)
        {
            return new AnalyzeWorkResponse(
                entity.ReportId,
                entity.AnalysisRecordId,
                entity.FileId,
                entity.IsPlagiarism,
                entity.SimilarityPercentage,
                entity.GeneratedAt
            );
        }

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