using Domain.Enums;

namespace Domain.Entities
{
    public class Work
    {
        public Guid WorkId { get; init; }

        public string StudentId { get; init; } = null!;
        public string AssignmentId { get; init; } = null!;

        public DateTime SubmissionTime { get; private set; }

        public string? FileId { get; private set; }

        public WorkStatus Status { get; private set; }

        public Guid? ReportId { get; private set; }
        public bool? PlagiarismFlag { get; private set; }

        public DateTime? AnalysisRequestedAt { get; private set; }
        public DateTime? AnalysisCompletedAt { get; private set; }

        public Work(string studentId, string assignmentId)
        {
            WorkId = Guid.NewGuid();
            StudentId = studentId;
            AssignmentId = assignmentId;
            SubmissionTime = DateTime.UtcNow;
            Status = WorkStatus.Created;
            
        }

        public void AttachFile(string fileId)
        {
            FileId = fileId;
            Status = WorkStatus.FileUploaded;
            
            // Invalidate Report
            ReportId = null;
            PlagiarismFlag = null;
            AnalysisRequestedAt = null;
            AnalysisCompletedAt = null;
        }

        public void AttachReport(Guid? reportId)
        {
            throw new NotImplementedException();
        }
    }
}