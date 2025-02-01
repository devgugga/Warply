using Warply.Communication.Request.Url;
using Warply.Communication.Response.Url;
using Warply.Domain;
using Warply.Domain.Enum;
using Warply.Domain.Repositories.Url;
using Warply.Domain.Repositories.Users;
using Warply.Exception.Exceptions;

namespace Warply.Application.UseCases.Register.Url;

internal class RegisterUrlUseCase(IUrlRepository repository, IUnityOfWork unityOfWork, IUsersRepository usersRepository)
    : IRegisterUrlUseCase
{
    public async Task<ResponseRegisterUrlJson> ExecuteAsync(RequestRegisterUrlJson request, Guid userId)
    {
        ValidateRequest(request);

        var user = await VerifyUserAsync(userId);

        var shortCode = await GenerateShortCode();

        user.Links += 1;

        var entity = new Domain.Entities.Url
        {
            UserId = user.Id,
            ShortCode = shortCode,
            OriginalUrl = request.OriginalUrl,
            ShortedUrl = $"https://warply.io/{shortCode}",
            UrlStatus = UrlStatus.Active
        };

        await repository.Add(entity);

        await unityOfWork.Commit();

        var response = new ResponseRegisterUrlJson
        {
            Id = entity.Id,
            ShortedUrl = entity.ShortedUrl
        };

        return response;
    }

    private static void ValidateRequest(RequestRegisterUrlJson request)
    {
        var validator = new RegisterUrlValidator();

        var result = validator.Validate(request);

        if (result.IsValid) return;

        var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }

    private async Task<Domain.Entities.User> VerifyUserAsync(Guid userId)
    {
        var user = await usersRepository.GetByIdAsync(userId);

        if (user is null)
            throw new NotFoundException("No user found with the specified id.");

        return user;
    }

    private async Task<string> GenerateShortCode(int maxAttempts = 5, int codeLength = 6)
    {
        var attempts = 0;
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        while (attempts < maxAttempts)
        {
            var shortCode = new string(Enumerable.Repeat(chars, codeLength)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

            if (await repository.GetUrlByShortCode(shortCode) is null)
                return shortCode;

            attempts++;

            if (attempts != maxAttempts - 1) continue;
            codeLength++;
            attempts = 0;
        }

        throw new InvalidException(
            "We couldn't create a short code after several attempts.");
    }
}