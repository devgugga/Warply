using Warply.Domain.Enum;

namespace Warply.Domain.Entities;

public class Url : BaseEntity
{
    public Guid UserId { get; set; }
    public string ShortCode { get; set; }
    public string OriginalUrl { get; set; }
    public string ShortedUrl { get; set; }
    public long LinkAccess { get; set; }
    public UrlStatus UrlStatus { get; set; }
    public List<string> UsersLocations { get; set; } = new();
}