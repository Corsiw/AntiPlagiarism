using Domain.Entities;
using Works.Application.Interfaces;

namespace Works.Application.UseCases.AttachFile
{
    public class AttachFileRequestHandler(IRepository<Work> repository, IFileStorageClient fileStorage) : IAttachFileRequestHandler
    {
        public async Task<AttachFileResponse> HandleAsync(Guid workId, AttachFileRequest request)
        {
            Work? work = await repository.GetAsync(workId);
            if (work == null)
            {
                throw new KeyNotFoundException("Work not found");
            }

            Guid fileId = await fileStorage.UploadAsync(
                request.FileStream,
                request.FileName,
                request.ContentType
                );
            
            work.AttachFile(fileId);
            await repository.UpdateAsync(work);

            return new AttachFileResponse(work.WorkId, fileId, work.Status.ToString());
        }
    }
}