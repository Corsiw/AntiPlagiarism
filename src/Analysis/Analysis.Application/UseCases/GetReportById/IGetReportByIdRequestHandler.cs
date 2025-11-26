namespace Analysis.Application.UseCases.GetReportById
{
    public interface IGetReportByIdRequestHandler
    {
        Task<GetReportByIdResponse?> HandleAsync(Guid workId);
    }
}