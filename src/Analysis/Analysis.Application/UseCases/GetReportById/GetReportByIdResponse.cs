namespace Analysis.Application.UseCases.GetReportById
{
    public record GetReportByIdResponse
    (
        Guid ReportId,
        Guid AnalysisRecordId,
        Guid FileId,
        bool IsPlagiarism,
        double SimilarityPercentage
    );
}