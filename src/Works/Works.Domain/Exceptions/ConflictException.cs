namespace Domain.Exceptions
{
    public class ConflictException(string? message = null) : Exception(message);
}