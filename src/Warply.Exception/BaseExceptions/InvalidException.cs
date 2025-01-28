namespace Warply.Exception.BaseExceptions;

public class InvalidException(string error) : WarplyException
{
    public string Message { get; set; } = error;
}