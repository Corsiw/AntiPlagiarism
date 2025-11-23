namespace Works.Application.UseCases.AddWork
{
    public record AddWorkRequest
    (
        string StudentId,
        string AssignmentId
    );
}