namespace Warply.Communication.Response.Error;

public class ResponseErrorJson
{
    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessage = [errorMessage];
    }

    public ResponseErrorJson(List<string> errorMessages)
    {
        ErrorMessage = errorMessages;
    }

    public List<string> ErrorMessage { get; set; }
}