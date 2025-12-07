namespace Works.Application.UseCases.GetWordCloud
{
    public record GetWordCloudResponse
    (
        Stream FileStream,
        string FileName,
        string ContentType
    );
}