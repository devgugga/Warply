using Warply.Communication.Response.User;

namespace Warply.Application.UseCases.Recive.User;

public interface IReciveUserUseCase
{
    Task<ResponseReciveUserJson> ExecuteAsync(Guid userId);
}