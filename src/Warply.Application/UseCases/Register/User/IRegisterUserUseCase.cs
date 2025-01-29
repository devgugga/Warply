using Warply.Communication.Request.User;
using Warply.Communication.Response.User;

namespace Warply.Application.UseCases.Register.User;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUserJson> ExecuteAsync(RequestRegisterUserJson request);
}