namespace Works.Application.UseCases.AnalyzeWork
{
    public record AnalyzeWorkResponse
    (
        Guid WorkId,
        string Status,
        Guid ReportId,
        Guid ReportFileId,
        bool PlagiarismFlag
    );
}