namespace Works.Application.UseCases.GetReport
{
    public record GetReportResponse
    (
        Guid WorkId,
        Guid ReportId,
        Guid ReportFileId,
        bool PlagiarismFlag
    );
}