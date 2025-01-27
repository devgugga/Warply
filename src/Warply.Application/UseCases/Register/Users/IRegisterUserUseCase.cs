using Warply.Communication.Request.User;
using Warply.Communication.Response.User;

namespace Warply.Application.UseCases.Users;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
}