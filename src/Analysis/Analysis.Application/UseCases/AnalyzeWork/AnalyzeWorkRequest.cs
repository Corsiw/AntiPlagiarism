namespace Analysis.Application.UseCases.AnalyzeWork
{
    public record AnalyzeWorkRequest(
        Guid WorkId,
        Guid FileId,
        string StudentId,
        string AssignmentId,
        DateTime SubmittedAt
    );
}