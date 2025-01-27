using Warply.Domain.Entities;
using Warply.Domain.Repositories.Users;

namespace Warply.Infrastructure.DataAccess.Repositories;

internal class UsersRepository(WarplyDbContext context) : IUsersRepository
{
    public async Task Add(User user)
    {
        await context.Users.AddAsync(user);
    }
}