using Analysis.Application.Interfaces;
using Analysis.Application.Mappers;
using Domain.Entities;

namespace Analysis.Application.UseCases.GetReportById
{
    public class GetReportByIdRequestHandler(IRepository<Report> repository, IReportMapper mapper)
        : IGetReportByIdRequestHandler
    {
        public async Task<GetReportByIdResponse?> HandleAsync(Guid reportId)
        {
            Report? report = await repository.GetAsync(reportId);
            return report == null ? null : mapper.MapEntityToGetReportByIdResponse(report);
        }
    }
}