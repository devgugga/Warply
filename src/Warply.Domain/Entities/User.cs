using Warply.Domain.Enum;

namespace Warply.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public List<Url> Urls { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool StayLoggedIn { get; set; }
    public PlanType PlanType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}