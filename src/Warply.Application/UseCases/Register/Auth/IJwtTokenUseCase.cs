using Warply.Communication.Request.Auth;
using Warply.Communication.Response.Auth;

namespace Warply.Application.UseCases.Register.Auth;

public interface IJwtTokenUseCase
{
    public Task<ResponseUserLoginJson> Login(RequestUserLoginJson request);
    public Task<ResponseUserLoginJson> RefreshToken(RequestUserRefreshTokenJson request);
}