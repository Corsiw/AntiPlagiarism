using Domain.Entities;
using Works.Application.Interfaces;

namespace Works.Application.UseCases.AnalyzeWork
{
    public class AnalyzeWorkRequestHandler(IRepository<Work> repository) : IAnalyzeWorkRequestHandler
    {
        public async Task<AnalyzeWorkResponse> HandleAsync(Guid workId)
        {
            Work? work = await repository.GetAsync(workId);
            if (work == null)
            {
                throw new KeyNotFoundException("Work not found");
            }
            
            // TODO analysis
            
            
            // work.AttachReport();
            // work.Status = WorkStatus.Analyzed;
            // work.ReportId = Guid.NewGuid();
            // work.PlagiarismFlag = false;
            // work.AnalysisRequestedAt = DateTime.UtcNow;
            // work.AnalysisCompletedAt = DateTime.UtcNow;

            await repository.UpdateAsync(work);

            return new AnalyzeWorkResponse(work.WorkId, work.Status.ToString(), work.ReportId);
        }
    }

}