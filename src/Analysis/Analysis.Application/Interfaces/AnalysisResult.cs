namespace Analysis.Application.Interfaces
{
    public class AnalysisResult(bool isPlagiarism, double similarity, Func<Task<MemoryStream>> generateReportFileAsync)
    {
        public bool IsPlagiarism { get; init; } = isPlagiarism;
        public double Similarity { get; init; } = similarity;

        public Func<Task<MemoryStream>> GenerateReportFileAsync { get; init; } = generateReportFileAsync;
    }

}