namespace Works.Application.UseCases.GetWorkById
{
    public record GetWorkByIdResponse
    (
        Guid WorkId,
        string StudentId,
        string AssignmentId,
        DateTime SubmissionTime,
        string? FileId,
        string Status,
        Guid? ReportId,
        bool? PlagiarismFlag
    );
}