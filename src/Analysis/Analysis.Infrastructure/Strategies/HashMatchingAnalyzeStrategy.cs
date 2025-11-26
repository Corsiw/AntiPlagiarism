using Analysis.Application.Interfaces;
using Domain.Entities;
using System.Text.Json;

namespace Analysis.Infrastructure.Strategies
{
    using System.Security.Cryptography;
    using System.Text;

    public class HashMatchingAnalyzeStrategy(IContentHashRepository hashRepo) : IAnalyzeStrategy
    {
        public async Task<AnalysisResult> AnalyzeAsync(Stream fileStream, AnalysisRecord analysisRecord)
        {
            string hash = await ComputeHashAsync(fileStream);

            ContentHashEntry? earlier = await hashRepo.FindEarlierSubmissionAsync(hash, analysisRecord.StudentId, analysisRecord.SubmittedAt);

            bool plagiarism = earlier != null;

            await hashRepo.AddAsync(new ContentHashEntry(
                hash,
                analysisRecord.AnalysisRecordId,
                analysisRecord.StudentId,
                analysisRecord.SubmittedAt
            ));

            return new AnalysisResult(
                plagiarism,
                1.0,
                () => GenerateJsonReportAsync(plagiarism, hash, earlier)
            );
        }

        private static async Task<string> ComputeHashAsync(Stream stream)
        {
            using SHA256 sha = SHA256.Create();
            stream.Position = 0;
            byte[] bytes = await sha.ComputeHashAsync(stream);
            return Convert.ToHexString(bytes);
        }

        private static Task<MemoryStream> GenerateJsonReportAsync(
            bool isPlagiarism,
            string hash,
            ContentHashEntry? earlier)
        {
            var jsonObj = new
            {
                IsPlagiarism = isPlagiarism,
                Hash = hash,
                EarlierSubmission = earlier is null
                    ? null
                    : new { earlier.StudentId, earlier.SubmittedAt }
            };

            string json = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });
            MemoryStream ms = new(Encoding.UTF8.GetBytes(json));
            ms.Position = 0;
            return Task.FromResult(ms);
        }
    }
}