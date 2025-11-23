namespace Works.Application.UseCases.ListWorks
{
    public interface IListWorksRequestHandler
    {
        Task<ListWorksResponse> HandleAsync();
    }
}