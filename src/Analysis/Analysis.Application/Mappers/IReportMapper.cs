using Analysis.Application.UseCases.GetReportById;
using Domain.Entities;

namespace Analysis.Application.Mappers
{
    public interface IReportMapper
    {
        GetReportByIdResponse MapEntityToGetReportByIdResponse(Report report);
    }
}