using Warply.Exception.BaseExceptions;

namespace Warply.Exception.Exceptions;

public class EmailAlreadyExistsException(string email) : WarplyException
{
    public string Email { get; set; } = email;
}