namespace Works.Application.UseCases.AddWork
{
    public record AddWorkResponse
    (
        Guid WorkId,
        string StudentId,
        string AssignmentId,
        DateTime SubmissionTime,
        string Status
    );
}