using Domain.Entities;
using Domain.Exceptions;
using Works.Application.DTO.Analysis;
using Works.Application.Interfaces;
using Works.Application.Mappers;

namespace Works.Application.UseCases.AnalyzeWork
{
    public class AnalyzeWorkRequestHandler(IRepository<Work> repository, IAnalysisClient analysisClient, IWorkMapper mapper) : IAnalyzeWorkRequestHandler
    {
        public async Task<AnalyzeWorkResponse> HandleAsync(Guid workId)
        {
            Work? work = await repository.GetAsync(workId);
            if (work == null)
            {
                throw new NotFoundException($"Work with workId {workId} not found");
            }

            AnalyzeWorkRequestDto analysisRequestDto = mapper.MapEntityToAnalyzeWorkRequest(work);
            AnalyzeWorkResponseDto response = await analysisClient.AnalyzeWork(analysisRequestDto);
            
            work.AttachReport(response.ReportId, response.IsPlagiarism);

            await repository.UpdateAsync(work);

            return new AnalyzeWorkResponse(work.WorkId, work.Status.ToString(), response.ReportId, response.FileId, response.IsPlagiarism);
        }
    }

}