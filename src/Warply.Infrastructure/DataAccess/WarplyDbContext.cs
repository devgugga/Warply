using Microsoft.EntityFrameworkCore;
using Warply.Domain.Entities;

namespace Warply.Infrastructure.DataAccess;

internal class WarplyDbContext(DbContextOptions<WarplyDbContext> options) : DbContext(options)
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Url> Urls { get; set; }
}