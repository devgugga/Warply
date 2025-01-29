using System.Security.Claims;
using Warply.Communication.Request.Auth;
using Warply.Communication.Response.Auth;
using Warply.Domain;
using Warply.Domain.Entities;
using Warply.Domain.Repositories.Users;
using Warply.Exception.Exceptions;
using Warply.Infrastructure.Security;

namespace Warply.Application.UseCases.Register.Auth;

internal class JwtTokenUseCase(
    ITokenService tokenService,
    IUsersRepository usersRepository,
    IPasswordHasher passwordHasher,
    IUnityOfWork unityOfWork) : IJwtTokenUseCase
{
    public async Task<ResponseUserLoginJson> Login(RequestUserLoginJson request)
    {
        ValidateLoginRequest(request);

        var user = await ValidatePassword(request);

        var token = tokenService.GenerateAccessToken(user);
        var refreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        await unityOfWork.Commit();

        var response = new ResponseUserLoginJson
        {
            AccessToken = token,
            RefreshToken = refreshToken
        };

        return response;
    }

    public async Task<ResponseUserLoginJson> RefreshToken(RequestUserRefreshTokenJson request)
    {
        ValidateRefreshTokenRequest(request);

        var principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        var userId = Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var user = await ValidateRefreshToken(request, userId);

        var newAccessToken = tokenService.GenerateAccessToken(user);
        var newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        await unityOfWork.Commit();

        var response = new ResponseUserLoginJson
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };

        return response;
    }

    private static void ValidateLoginRequest(RequestUserLoginJson request)
    {
        var validator = new JwtTokenValidator();

        var result = validator.Validate(request);

        if (result.IsValid) return;

        var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }

    private static void ValidateRefreshTokenRequest(RequestUserRefreshTokenJson request)
    {
        var validator = new RefreshTokenValidator();

        var result = validator.Validate(request);

        if (result.IsValid) return;

        var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }

    private async Task<User> ValidatePassword(RequestUserLoginJson request)
    {
        var user = await usersRepository.GetByEmailAsync(request.Email);
        if (user == null || !passwordHasher.VerifyHashedPassword(user.PasswordHash, request.Password))
            throw new NotFoundException("User not found.");

        return user;
    }

    private async Task<User> ValidateRefreshToken(RequestUserRefreshTokenJson request, Guid userId)
    {
        var user = await usersRepository.GetByIdAsync(userId);
        if (user == null ||
            user.RefreshToken != request.RefreshToken ||
            user.RefreshTokenExpiry <= DateTime.UtcNow)
            throw new InvalidException("Invalid Refresh Token.");

        return user;
    }
}