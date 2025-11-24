
namespace FileStorage.Application.UseCases.GetFileById
{
    public interface IGetFileByIdRequestHandler
    {
        Task<GetFileByIdResponse?> HandleAsync(Guid fileId);
    }
}