using Analysis.Application.Interfaces;
using Domain.Entities;

namespace Analysis.Infrastructure.Analyzers
{
    public class AnalyzeProvider(IFileStorageClient fileStorage, IAnalyzeStrategy strategy) : IAnalyzeProvider
    {
        public async Task<Report> AnalyzeAsync(AnalysisRecord record)
        {
            await using Stream fileStream = await fileStorage.DownloadAsync(record.FileId);

            AnalysisResult result = await strategy.AnalyzeAsync(fileStream, record);

            using MemoryStream reportFile = await result.GenerateReportFileAsync();

            Guid savedReportFileId = await fileStorage.UploadAsync(
                reportFile,
                $"report_{record.AnalysisRecordId}.json",
                "application/json"
            );

            Report report = new(
                record.AnalysisRecordId,
                savedReportFileId,
                result.IsPlagiarism,
                result.Similarity
            );

            return report;
        }
    }
}