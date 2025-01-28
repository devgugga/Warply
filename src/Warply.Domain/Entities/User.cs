using Warply.Domain.Enum;

namespace Warply.Domain.Entities;

public class User : BaseEntity
{
    public List<Url> Urls { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool StayLoggedIn { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public PlanType PlanType { get; set; }
}