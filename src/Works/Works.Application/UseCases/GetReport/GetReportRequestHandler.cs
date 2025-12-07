using Domain.Entities;
using Domain.Exceptions;
using Works.Application.DTO.Analysis;
using Works.Application.Interfaces;

namespace Works.Application.UseCases.GetReport
{
    public class GetReportRequestHandler(IRepository<Work> repository, IAnalysisClient analysisClient) : IGetReportRequestHandler
    {
        public async Task<GetReportResponse?> HandleAsync(Guid workId)
        {
            Work? work = await repository.GetAsync(workId);

            Guid reportId = work?.ReportId ?? throw  new NotFoundException($"Report for Work with ID {workId} was not found");
            GetReportByIdResponseDto response = await analysisClient.GetReportById(reportId);

            return new GetReportResponse(
                workId,
                response.ReportId,
                response.FileId,
                response.IsPlagiarism
            );
        }
    }

}