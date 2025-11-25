namespace Domain.Entities
{
    public class WorkAnalysis
    {
        public Guid AnalysisId { get; init; }

        public string WorkId { get; init; }
        public string FileId { get; init; }

        public string? ReportFileId { get; private set; }
        public bool? PlagiarismFlag { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public WorkAnalysis(string workId, string fileId)
        {
            AnalysisId = Guid.NewGuid();
            WorkId = workId;
            FileId = fileId;
            CreatedAt = DateTime.UtcNow;
        }

        public void AttachReportFile(string reportFileId, bool plagiarismFlag)
        {
            ReportFileId = reportFileId;
            PlagiarismFlag = plagiarismFlag;
        }
    }
}
