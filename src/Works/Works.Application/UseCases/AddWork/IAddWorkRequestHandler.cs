namespace Works.Application.UseCases.AddWork
{
    public interface IAddWorkRequestHandler
    {
        Task<AddWorkResponse> HandleAsync(AddWorkRequest request);
    }
}