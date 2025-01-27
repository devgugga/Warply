using Warply.Communication.Enums;

namespace Warply.Communication.Request.User;

public class RequestRegisterUserJson
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public PlanType PlanType { get; set; }
}