namespace Works.Application.UseCases.ListWorks
{
    public record ListWorksResponseItem
    (
        Guid WorkId,
        string StudentId,
        string AssignmentId,
        DateTime SubmissionTime,
        string? Status,
        Guid? ReportId,
        bool? PlagiarismFlag
    );

    public record ListWorksResponse(IReadOnlyList<ListWorksResponseItem> Works);
}