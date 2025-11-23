namespace Works.Application.UseCases.GetReport
{
    public interface IGetReportRequestHandler
    {
        Task<GetReportResponse?> HandleAsync(Guid workId);
    }
}