using Domain.Entities;
using Works.Application.DTO.Analysis;
using Works.Application.UseCases.AddWork;
using Works.Application.UseCases.GetWorkById;
using Works.Application.UseCases.ListWorks;

namespace Works.Application.Mappers
{
    public interface IWorkMapper
    {
        Work MapAddWorkRequestToEntity(AddWorkRequest request);
        AddWorkResponse MapEntityToAddWorkResponse(Work work);
        ListWorksResponseItem MapEntityToListWorksResponseItem(Work work);
        GetWorkByIdResponse MapEntityToGetWorkByIdResponse(Work work);
        AnalyzeWorkRequestDto MapEntityToAnalyzeWorkRequest(Work work);
    }
}