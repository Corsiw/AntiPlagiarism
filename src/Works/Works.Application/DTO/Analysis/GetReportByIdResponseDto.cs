namespace Works.Application.DTO.Analysis
{
    public record GetReportByIdResponseDto
    (
        Guid ReportId,
        Guid AnalysisRecordId,
        Guid FileId,
        bool IsPlagiarism,
        double SimilarityPercentage,
        DateTime GeneratedAt
    );
}