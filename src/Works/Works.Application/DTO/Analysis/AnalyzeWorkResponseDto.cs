namespace Works.Application.DTO.Analysis
{
    public record AnalyzeWorkResponseDto(
        Guid ReportId,
        Guid AnalysisRecordId,
        Guid FileId,
        bool IsPlagiarism,
        double SimilarityPercentage,
        DateTime GeneratedAt
    );
}