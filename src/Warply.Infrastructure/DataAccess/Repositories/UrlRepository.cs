using Microsoft.EntityFrameworkCore;
using Warply.Domain.Entities;
using Warply.Domain.Repositories.Url;

namespace Warply.Infrastructure.DataAccess.Repositories;

internal class UrlRepository(WarplyDbContext context) : IUrlRepository
{
    public async Task Add(Url url)
    {
        await context.Urls.AddAsync(url);
    }

    public async Task<Url?> GetUrlByShortCode(string shortCode)
    {
        return await context.Urls.AsNoTracking().FirstOrDefaultAsync(url => url.ShortCode == shortCode);
    }
}