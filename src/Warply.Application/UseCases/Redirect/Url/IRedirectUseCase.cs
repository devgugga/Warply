using Warply.Communication.Response.Url;

namespace Warply.Application.UseCases.Redirect.Url;

public interface IRedirectUseCase
{
    public Task<ResponseRedirectUrlJson> Redirect(string shortCode, string ip);
}