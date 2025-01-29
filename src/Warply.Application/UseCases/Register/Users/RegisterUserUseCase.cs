using Warply.Application.UseCases.Users;
using Warply.Communication.Request.User;
using Warply.Communication.Response.User;
using Warply.Domain;
using Warply.Domain.Entities;
using Warply.Domain.Enum;
using Warply.Domain.Repositories.Users;
using Warply.Domain.Services.Utils;
using Warply.Exception.Exceptions;
using Warply.Infrastructure.Security;

namespace Warply.Application.UseCases.Register.Users;

internal class RegisterUserUseCase(
    IUsersRepository repository,
    IUnityOfWork unityOfWork,
    IPasswordHasher passwordHasher,
    ITokenService tokenService,
    ICloudflareClient cloudflareClient) : IRegisterUserUseCase
{
    public async Task<ResponseRegisterUserJson> ExecuteAsync(RequestRegisterUserJson request)
    {
        ValidateRequest(request);

        await ValidateEmail(request.Email);

        var newPasswordHash = passwordHasher.HashPassword(request.Password);

        var nickName = await GenerateNickName();

        var entity = new User
        {
            Name = request.Name,
            NickName = nickName,
            Email = request.Email,
            PasswordHash = newPasswordHash,
            PlanType = (PlanType)request.PlanType
        };

        var tokens = new Dictionary<string, string>
        {
            { "AccessToken", tokenService.GenerateAccessToken(entity) },
            { "RefreshToken", tokenService.GenerateRefreshToken() }
        };

        entity.RefreshToken = tokens["RefreshToken"];
        entity.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        await repository.Add(entity);

        await unityOfWork.Commit();

        var response = new ResponseRegisterUserJson
        {
            Id = entity.Id,
            Name = entity.Name,
            NickName = entity.NickName,
            Email = entity.Email,
            PlanType = (Communication.Enums.PlanType)entity.PlanType,
            Tokens = tokens
        };

        return response;
    }

    private static void ValidateRequest(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        if (result.IsValid) return;

        var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }

    private async Task ValidateEmail(string email)
    {
        var existingUser = await repository.GetByEmailAsync(email);

        if (existingUser != null) throw new EmailAlreadyExistsException(email);
    }

    private async Task<string> GenerateNickName(int maxAttempts = 5)
    {
        var attempts = 0;
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        while (attempts < maxAttempts)
        {
            var nick = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());

            if (await repository.GetByNickNameAsync(nick) is null)
                return nick;

            attempts++;
        }

        throw new InvalidException("We couldn't create a nickname after several attempts.");
    }
}