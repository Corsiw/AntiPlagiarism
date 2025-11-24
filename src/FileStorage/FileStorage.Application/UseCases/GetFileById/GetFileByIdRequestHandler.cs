using Domain.Entities;
using FileStorage.Application.Interfaces;
using FileStorage.Application.Mappers;

namespace FileStorage.Application.UseCases.GetFileById
{
    public class GetFileByIdRequestHandler(IRepository<FileRecord> repository, IFileRecordMapper mapper)
        : IGetFileByIdRequestHandler
    {
        public async Task<GetFileByIdResponse?> HandleAsync(Guid fileId)
        {
            FileRecord? fileRecord = await repository.GetAsync(fileId);
            return fileRecord == null ? null : await mapper.MapEntityToGetFileByIdResponse(fileRecord);
        }
    }
}