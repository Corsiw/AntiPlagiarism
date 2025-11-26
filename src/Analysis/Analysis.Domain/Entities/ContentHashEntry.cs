namespace Domain.Entities
{
    public class ContentHashEntry(string hash, Guid analysisRecordId, string studentId, DateTime submittedAt)
    {
        public Guid HashEntryId { get; init; } = Guid.NewGuid();
        public string Hash { get; init; } = hash;
        public Guid AnalysisRecordId { get; init; } = analysisRecordId;
        public string StudentId { get; init; } = studentId;
        public DateTime SubmittedAt { get; init; } = submittedAt;
    }
}