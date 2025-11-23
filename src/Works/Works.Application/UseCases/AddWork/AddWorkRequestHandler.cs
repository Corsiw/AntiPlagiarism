using Domain.Entities;
using Works.Application.Interfaces;
using Works.Application.Mappers;

namespace Works.Application.UseCases.AddWork
{
    public class AddWorkRequestHandler(IRepository<Work> repository, IWorkMapper mapper) : IAddWorkRequestHandler
    {
        public async Task<AddWorkResponse> HandleAsync(AddWorkRequest request)
        {
            Work work = mapper.MapAddWorkRequestToEntity(request);

            await repository.AddAsync(work);

            return mapper.MapEntityToAddWorkResponse(work);
        }
    }
}