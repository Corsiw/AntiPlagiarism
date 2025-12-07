using Domain.Entities;
using Domain.Exceptions;
using Works.Application.Interfaces;

namespace Works.Application.UseCases.GetWordCloud
{
    public class GetWordCloudRequestHandler(
        IRepository<Work> repository,
        IFileStorageClient fileStorageClient,
        IWordCloudClient wordCloudClient
    ) : IGetWordCloudRequestHandler
    {
        public async Task<GetWordCloudResponse> HandleAsync(Guid workId)
        {
            Work? work = await repository.GetAsync(workId);
            if (work?.FileId == null)
            {
                throw new NotFoundException($"File for Work with ID {workId} was not found");
            }

            Guid workFileId = work.FileId.Value;

            await using Stream fileStream = await fileStorageClient.DownloadAsync(workFileId);

            Stream wordCloudStream = await wordCloudClient.GetAsync(fileStream);
            
            return new GetWordCloudResponse(wordCloudStream, $"WordCloud_{workFileId}.svg", "image/svg");
        }
    }
}