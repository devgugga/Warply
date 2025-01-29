using Warply.Exception.BaseExceptions;

namespace Warply.Exception.Exceptions;

public class ErrorOnValidationException(List<string> errorMessages) : WarplyException
{
    public List<string> Errors { get; set; } = errorMessages;
}