using Domain.Entities;
using FileStorage.Application.Interfaces;
using FileStorage.Application.UseCases.GetFileById;
using FileStorage.Application.UseCases.UploadFile;

namespace FileStorage.Application.Mappers
{
    public class FileRecordMapper(IFileStorageProvider storage) : IFileRecordMapper
    {
        public async Task<FileRecord> MapUploadFileRequestToEntityAsync(UploadFileRequest request)
        {
            string storagePath = await storage.SaveAsync(
                request.FileStream,
                request.FileName,
                request.ContentType
            );
            
            FileRecord fileRecord = new(
                request.FileName,
                request.ContentType,
                request.FileStream.Length,
                storagePath
            );

            return fileRecord;
        }

        public UploadFileResponse MapEntityToUploadFileResponse(FileRecord fileRecord)
        {
            return new UploadFileResponse(
                fileRecord.FileId,
                fileRecord.FileName,
                fileRecord.StoragePath
            );
        }

        public async Task<GetFileByIdResponse> MapEntityToGetFileByIdResponse(FileRecord record)
        {
            Stream stream = await storage.GetAsync(record.StoragePath);
            
            return new GetFileByIdResponse(
                stream,
                record.FileName,
                record.ContentType
            );
        }
    }
}