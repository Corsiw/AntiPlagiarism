using Domain.Entities;
using Works.Application.Interfaces;
using Works.Application.Mappers;

namespace Works.Application.UseCases.GetWorkById
{
    public class GetWorkByIdRequestHandler(IRepository<Work> repository, IWorkMapper mapper)
        : IGetWorkByIdRequestHandler
    {
        public async Task<GetWorkByIdResponse?> HandleAsync(Guid workId)
        {
            Work? work = await repository.GetAsync(workId);
            return work == null ? null : mapper.MapEntityToGetWorkByIdResponse(work);
        }
    }
}