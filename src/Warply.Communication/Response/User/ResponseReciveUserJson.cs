namespace Warply.Communication.Response.User;

public class ResponseReciveUserJson
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string AvatarUrl { get; set; }
    public string Nickname { get; set; }
    public string PlanType { get; set; }
    public long Links { get; set; }
}