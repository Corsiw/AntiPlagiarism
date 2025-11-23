using Domain.Entities;
using Domain.Enums;
using Works.Application.Interfaces;

namespace Works.Application.UseCases.AttachFile
{
    public class AttachFileRequestHandler(IRepository<Work> repository) : IAttachFileRequestHandler
    {
        public async Task<AttachFileResponse> HandleAsync(Guid workId, AttachFileRequest request)
        {
            Work? work = await repository.GetAsync(workId);
            if (work == null)
            {
                throw new KeyNotFoundException("Work not found");
            }

            work.FileId = request.FileId;
            work.Status = WorkStatus.FileUploaded;

            await repository.UpdateAsync(work);

            return new AttachFileResponse(work.WorkId, work.FileId!, work.Status.ToString());
        }
    }

}