namespace Warply.Domain.Entities;

public class Url : BaseEntity
{
    public string ShortCode { get; set; }
    public string OriginalUrl { get; set; }
}