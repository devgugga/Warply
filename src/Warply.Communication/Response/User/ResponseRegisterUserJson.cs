using Warply.Communication.Enums;

namespace Warply.Communication.Response.User;

public class ResponseRegisterUserJson
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public PlanType PlanType { get; set; }
    public Dictionary<string, string> Tokens { get; set; }
}