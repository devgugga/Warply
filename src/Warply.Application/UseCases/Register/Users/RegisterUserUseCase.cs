using Warply.Application.UseCases.Users;
using Warply.Communication.Request.User;
using Warply.Communication.Response.User;
using Warply.Domain;
using Warply.Domain.Entities;
using Warply.Domain.Enum;
using Warply.Domain.Repositories.Users;
using Warply.Exception.BaseExceptions;
using Warply.Infrastructure.Security;

namespace Warply.Application.UseCases.Register.Users;

internal class RegisterUserUseCase(
    IUsersRepository repository,
    IUnityOfWork unityOfWork,
    IPasswordHasher passwordHasher) : IRegisterUserUseCase
{
    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        ValidateRequest(request);

        await ValidateEmail(request.Email);

        var newPasswordHash = passwordHasher.HashPassword(request.Password);

        var entity = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = newPasswordHash,
            PlanType = (PlanType)request.PlanType
        };

        await repository.Add(entity);

        await unityOfWork.Commit();

        var response = new ResponseRegisterUserJson
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            PlanType = (Communication.Enums.PlanType)entity.PlanType
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
}