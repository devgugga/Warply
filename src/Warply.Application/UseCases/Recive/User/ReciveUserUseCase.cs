using Warply.Communication.Response.User;
using Warply.Domain.Repositories.Users;
using Warply.Exception.Exceptions;

namespace Warply.Application.UseCases.Recive.User;

internal class ReciveUserUseCase(IUsersRepository repository) : IReciveUserUseCase
{
    public async Task<ResponseReciveUserJson> ExecuteAsync(Guid userId)
    {
        var user = await VerifyUserAsync(userId);

        var entity = new ResponseReciveUserJson
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            AvatarUrl = user.Avatar,
            Nickname = user.NickName,
            PlanType = user.PlanType.ToString(),
            Links = user.Links
        };

        return entity;
    }

    private async Task<Domain.Entities.User> VerifyUserAsync(Guid userId)
    {
        var user = await repository.GetByIdAsync(userId);

        if (user is null)
            throw new NotFoundException("No user found with the specified id.");

        return user;
    }
}