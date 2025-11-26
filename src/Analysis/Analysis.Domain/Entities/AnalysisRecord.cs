using Domain.Enums;

namespace Domain.Entities
{
    public class AnalysisRecord(Guid workId, Guid fileId, string studentId, string assignmentId, DateTime submittedAt)
    {
        public Guid AnalysisRecordId { get; init; } = Guid.NewGuid();

        public Guid WorkId { get; init; } = workId;
        public Guid FileId { get; init; } = fileId;
        public string StudentId { get; init; } = studentId;
        public string AssignmentId { get; init; } = assignmentId;
        public DateTime SubmittedAt { get; init; } = submittedAt;

        public AnalysisStatus Status { get; private set; } = AnalysisStatus.Created;
        public Guid? ReportId { get; private set; }

        public void SetStatusRunning()
        {
            Status = AnalysisStatus.Running;
        }
        
        public void AttachReport(Guid reportId)
        {
            ReportId = reportId;

            Status = AnalysisStatus.Done;
        }

        public void SetStatusFailed()
        {
            Status = AnalysisStatus.Failed;
        }
    }
}