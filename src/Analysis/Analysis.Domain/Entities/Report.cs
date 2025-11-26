namespace Domain.Entities
{
    public class Report(Guid analysisRecordId, Guid fileId, bool isPlagiarism, double similarityPercentage)
    {
        public Guid ReportId { get; init; } = Guid.NewGuid();

        public Guid AnalysisRecordId { get; init; } = analysisRecordId;
        public Guid FileId { get; init; } = fileId;

        public bool IsPlagiarism { get; init; } = isPlagiarism;
        public double SimilarityPercentage { get; init; } = similarityPercentage;

        public DateTime GeneratedAt { get; init; } = DateTime.UtcNow;
    }
}