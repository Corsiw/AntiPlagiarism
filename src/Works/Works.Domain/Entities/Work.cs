using Domain.Enums;

namespace Domain.Entities
{
    public class Work
    {
        public Guid WorkId { get; set; }

        public string StudentId { get; set; } = null!;
        public string AssignmentId { get; set; } = null!;

        public DateTime SubmissionTime { get; set; }

        public string? FileId { get; set; }

        public WorkStatus Status { get; set; }

        public Guid? ReportId { get; set; }
        public bool? PlagiarismFlag { get; set; }

        public DateTime? AnalysisRequestedAt { get; set; }
        public DateTime? AnalysisCompletedAt { get; set; }
    }
}