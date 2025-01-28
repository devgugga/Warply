namespace Warply.Exception.BaseExceptions;

public class NotFoundException(string error) : WarplyException
{
    public string Message { get; set; } = error;
}