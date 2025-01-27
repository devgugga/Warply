using Warply.Domain;

namespace Warply.Infrastructure.DataAccess;

internal class UnitOfWork(WarplyDbContext context) : IUnityOfWork
{
    public async Task Commit()
    {
        await context.SaveChangesAsync();
    }
}