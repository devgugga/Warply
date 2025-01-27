namespace Warply.Exception.BaseExceptions;

public class EmailAlreadyExistsException(string email) : WarplyException
{
    public string Email { get; set; } = email;
}