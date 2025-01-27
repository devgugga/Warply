using Microsoft.Extensions.DependencyInjection;
using Warply.Application.UseCases.Register.Users;
using Warply.Application.UseCases.Users;

namespace Warply.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}