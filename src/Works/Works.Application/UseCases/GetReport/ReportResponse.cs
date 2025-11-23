namespace Works.Application.UseCases.GetReport
{
    public record ReportItem
    (
        string? MatchedWorkId,
        string? Snippet,
        double? MatchPercent
    );

    public record GetReportResponse
    (
        Guid WorkId,
        double Score,
        bool IsPlagiarism,
        IEnumerable<ReportItem>? Matches,
        string? WordCloudUrl
    );
}