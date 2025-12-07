namespace Works.Application.UseCases.GetWordCloud
{
    public interface IGetWordCloudRequestHandler
    {
        Task<GetWordCloudResponse> HandleAsync(Guid workId);
    }
}