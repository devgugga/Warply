using Warply.Domain.Entities;

namespace Warply.Domain.Repositories.Users;

public interface IUsersRepository
{
    Task Add(User user);
}