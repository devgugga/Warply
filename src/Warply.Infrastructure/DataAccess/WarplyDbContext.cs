using Microsoft.EntityFrameworkCore;
using Warply.Domain.Entities;

namespace Warply.Infrastructure.DataAccess;

internal class WarplyDbContext(DbContextOptions<WarplyDbContext> options) : DbContext(options)
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Url> Urls { get; set; }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("CreatedAt") != null))
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Property("CreatedAt").IsModified = false;
                    break;
            }

            entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}