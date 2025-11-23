namespace Works.Application.UseCases.AnalyzeWork
{
    public interface IAnalyzeWorkRequestHandler
    {
        Task<AnalyzeWorkResponse> HandleAsync(Guid workId);
    }
}