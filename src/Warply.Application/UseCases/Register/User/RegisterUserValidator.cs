using FluentValidation;
using Warply.Communication.Request.User;

namespace Warply.Application.UseCases.Register.User;

internal class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required.");
        RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required.");
        RuleFor(user => user.PlanType).IsInEnum().WithMessage("This plan type is invalid.");
    }
}