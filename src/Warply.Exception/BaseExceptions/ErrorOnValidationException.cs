namespace Warply.Exception.BaseExceptions;

public class ErrorOnValidationException(List<string> errorMessages) : WarplyException
{
    public List<string> Errors { get; set; } = errorMessages;
}