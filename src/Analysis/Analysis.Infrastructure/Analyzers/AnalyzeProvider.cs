using Analysis.Application.Interfaces;
using Domain.Entities;

namespace Analysis.Infrastructure.Analyzers
{
    public class AnalyzeProvider(IFileStorageClient fileStorage, IAnalyzeStrategy strategy) : IAnalyzeProvider
    {
        public async Task<Report> AnalyzeAsync(AnalysisRecord record)
        {
            record.SetStatusRunning();
            
            await using Stream fileStream = await fileStorage.DownloadAsync(record.FileId);

            AnalysisResult analysisResult = await strategy.AnalyzeAsync(fileStream, record);

            using MemoryStream reportFile = await analysisResult.GenerateReportFileAsync();

            Guid savedReportFileId = await fileStorage.UploadAsync(
                reportFile,
                analysisResult.FileName,
                analysisResult.ContentType
            );

            Report report = new(
                record.AnalysisRecordId,
                savedReportFileId,
                analysisResult.IsPlagiarism,
                analysisResult.Similarity
            );
            
            record.AttachReport(report.ReportId);
            return report;
        }
    }
}