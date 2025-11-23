namespace Works.Application.UseCases.GetWorkById
{
    public interface IGetWorkByIdRequestHandler
    {
        Task<GetWorkByIdResponse?> HandleAsync(Guid workId);
    }
}