using Domain.Entities;
using Works.Application.Interfaces;

namespace Works.Application.UseCases.GetReport
{
    public class GetReportRequestHandler(IRepository<Work> repository) : IGetReportRequestHandler
    {
        public async Task<GetReportResponse?> HandleAsync(Guid workId)
        {
            Work? work = await repository.GetAsync(workId);
            if (work?.ReportId == null)
            {
                return null;
            }

            // TODO обращение к Analysis
            return new GetReportResponse(
                work.WorkId,
                Score: 0,
                IsPlagiarism: work.PlagiarismFlag ?? false,
                Matches: [],
                WordCloudUrl: null
            );
        }
    }

}