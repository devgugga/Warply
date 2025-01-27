namespace Warply.Domain.Entities;

public class Url
{
    public Guid Id { get; set; }
    public string ShortCode { get; set; }
    public string OriginalUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}