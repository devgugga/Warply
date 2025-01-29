using Warply.Exception.BaseExceptions;

namespace Warply.Exception.Exceptions;

public class UploadException(string error) : WarplyException
{
    public string Message { get; set; } = error;
}