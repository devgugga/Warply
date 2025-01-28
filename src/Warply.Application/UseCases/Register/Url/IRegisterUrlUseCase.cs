using Warply.Communication.Request.Url;
using Warply.Communication.Response.Url;

namespace Warply.Application.UseCases.Register.Url;

public interface IRegisterUrlUseCase
{
    Task<ResponseRegisterUrlJson> ExecuteAsync(RequestRegisterUrlJson request, Guid userId);
}