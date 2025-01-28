using Microsoft.Extensions.DependencyInjection;
using Warply.Application.UseCases.Redirect.Url;
using Warply.Application.UseCases.Register.Auth;
using Warply.Application.UseCases.Register.Url;
using Warply.Application.UseCases.Register.Users;
using Warply.Application.UseCases.Users;

namespace Warply.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        service.AddScoped<IRegisterUrlUseCase, RegisterUrlUseCase>();
        service.AddScoped<IRedirectUseCase, RedirectUseCase>();
        service.AddScoped<IJwtTokenUseCase, JwtTokenUseCase>();
    }
}