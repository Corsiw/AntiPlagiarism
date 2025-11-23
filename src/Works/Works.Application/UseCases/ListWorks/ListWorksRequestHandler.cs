using Domain.Entities;
using Works.Application.Interfaces;
using Works.Application.Mappers;

namespace Works.Application.UseCases.ListWorks
{
    public class ListWorksRequestHandler(IRepository<Work> repository, IWorkMapper mapper) : IListWorksRequestHandler
    {
        public async Task<ListWorksResponse> HandleAsync()
        {
            IEnumerable<Work> works = await repository.ListAsync();
            List<ListWorksResponseItem> items = works.Select(mapper.MapEntityToListWorksResponseItem).ToList();
            return new ListWorksResponse(items);
        }
    }
}