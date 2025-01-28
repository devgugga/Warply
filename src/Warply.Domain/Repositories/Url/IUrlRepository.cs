namespace Warply.Domain.Repositories.Url;

public interface IUrlRepository
{
    Task Add(Entities.Url url);

    Task<Entities.Url?> GetUrlByShortCode(string shortCode);
}