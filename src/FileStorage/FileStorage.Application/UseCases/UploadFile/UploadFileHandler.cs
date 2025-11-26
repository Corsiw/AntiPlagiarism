using Domain.Entities;
using FileStorage.Application.Interfaces;
using FileStorage.Application.Mappers;

namespace FileStorage.Application.UseCases.UploadFile
{
    public class UploadFileHandler(IRepository<FileRecord> repository, IFileRecordMapper mapper)
        : IUploadFileHandler
    {
        public async Task<UploadFileResponse> HandleAsync(UploadFileRequest request)
        {
            FileRecord fileRecord = await mapper.MapUploadFileRequestToEntityAsync(request);
            
            await repository.AddAsync(fileRecord);

            return new UploadFileResponse(fileRecord.FileId, fileRecord.FileName, fileRecord.StoragePath);
        }
    }
}