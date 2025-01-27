namespace Warply.Domain.Entities;

public class Url : BaseEntity
{
    public Guid Id { get; set; }
    public string ShortCode { get; set; }
    public string OriginalUrl { get; set; }
}