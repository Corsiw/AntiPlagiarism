namespace FileStorage.Application.UseCases.GetFileById
{
    public record GetFileByIdResponse
    (
        Stream FileStream,
        string FileName,
        string ContentType
    );
}