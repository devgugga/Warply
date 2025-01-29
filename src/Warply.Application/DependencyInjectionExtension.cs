using Microsoft.Extensions.DependencyInjection;
using Warply.Application.UseCases.Redirect.Url;
using Warply.Application.UseCases.Register.Auth;
using Warply.Application.UseCases.Register.Url;
using Warply.Application.UseCases.Register.User;
using Warply.Application.UseCases.Update.User;
using Warply.Application.UseCases.Utils;
using Warply.Domain.Services.Utils;

namespace Warply.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        service.AddScoped<IRegisterUrlUseCase, RegisterUrlUseCase>();
        service.AddScoped<IRedirectUseCase, RedirectUseCase>();
        service.AddScoped<IJwtTokenUseCase, JwtTokenUseCase>();
        service.AddScoped<ICloudflareClient, CloudflareClientUseCase>();
        service.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
    }
}