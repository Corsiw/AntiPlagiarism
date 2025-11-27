using Domain.Enums;

namespace Domain.Entities
{
    public class Work(string studentId, string assignmentId)
    {
        public Guid WorkId { get; init; } = Guid.NewGuid();

        public string StudentId { get; init; } = studentId;
        public string AssignmentId { get; init; } = assignmentId;

        public DateTime SubmissionTime { get; private set; } = DateTime.UtcNow;

        public Guid? FileId { get; private set; }

        public WorkStatus Status { get; private set; } = WorkStatus.Created;

        public Guid? ReportId { get; private set; }
        public bool? PlagiarismFlag { get; private set; }
        
        public void AttachFile(Guid fileId)
        {
            FileId = fileId;
            Status = WorkStatus.FileUploaded;

            // Invalidate Report
            ReportId = null;
            PlagiarismFlag = null;
        }
        
        public void AttachReport(Guid reportId, bool plagiarismFlag)
        {
            ReportId = reportId;
            PlagiarismFlag = plagiarismFlag;

            Status = WorkStatus.Analyzed;
        }
    }
}
