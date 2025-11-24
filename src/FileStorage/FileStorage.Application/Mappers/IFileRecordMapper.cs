using Domain.Entities;
using FileStorage.Application.UseCases.GetFileById;
using FileStorage.Application.UseCases.UploadFile;

namespace FileStorage.Application.Mappers
{
    public interface IFileRecordMapper
    {
        Task<FileRecord> MapUploadFileRequestToEntityAsync(UploadFileRequest request);
        UploadFileResponse MapEntityToUploadFileResponse(FileRecord fileRecord);
        Task<GetFileByIdResponse> MapEntityToGetFileByIdResponse(FileRecord record);
    }
}