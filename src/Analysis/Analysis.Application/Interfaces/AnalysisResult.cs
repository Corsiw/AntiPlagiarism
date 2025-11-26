namespace Analysis.Application.Interfaces
{
    public class AnalysisResult(bool isPlagiarism, double similarity, string fileName, string contentType, Func<Task<MemoryStream>> generateReportFileAsync)
    {
        public bool IsPlagiarism { get; init; } = isPlagiarism;
        public double Similarity { get; init; } = similarity;

        public string FileName { get; init; } = fileName;
        public string ContentType { get; init; } = contentType;
        public Func<Task<MemoryStream>> GenerateReportFileAsync { get; init; } = generateReportFileAsync;
    }

}