namespace Analysis.Application.UseCases.AnalyzeWork
{
    public record AnalyzeWorkResponse(
        Guid ReportId,
        Guid AnalysisRecordId,
        Guid FileId,
        bool IsPlagiarism,
        double SimilarityPercentage,
        DateTime GeneratedAt
    );
}