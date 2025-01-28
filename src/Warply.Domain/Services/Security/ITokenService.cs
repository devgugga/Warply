using System.Security.Claims;
using Warply.Domain.Entities;

namespace Warply.Infrastructure.Security;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}