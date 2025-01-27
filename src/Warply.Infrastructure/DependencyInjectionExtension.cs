using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warply.Domain;
using Warply.Domain.Repositories.Users;
using Warply.Infrastructure.DataAccess;
using Warply.Infrastructure.DataAccess.Repositories;
using Warply.Infrastructure.Security;

namespace Warply.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUnityOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasher, Argon2IdPasswordHasher>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WarplyDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}