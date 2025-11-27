namespace Works.Application.DTO.Analysis
{
    public record AnalyzeWorkRequestDto(
        Guid WorkId,
        Guid FileId,
        string StudentId,
        string AssignmentId,
        DateTime SubmittedAt
    );
}