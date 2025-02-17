using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warply.Domain;
using Warply.Domain.Repositories.Url;
using Warply.Domain.Repositories.Users;
using Warply.Domain.Services.Security;
using Warply.Infrastructure.DataAccess;
using Warply.Infrastructure.DataAccess.Repositories;
using Warply.Infrastructure.Security;

namespace Warply.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddSecurity(services);
        AddDbContext(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUrlRepository, UrlRepository>();
        services.AddScoped<IUnityOfWork, UnitOfWork>();
    }

    private static void AddSecurity(IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher, Argon2IdPasswordHasher>();
        services.AddScoped<ITokenService, JwtTokenService>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WarplyDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}