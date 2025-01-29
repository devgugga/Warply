using FluentValidation;
using Warply.Communication.Request.User;

namespace Warply.Application.UseCases.Update.User;

internal class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(u => u.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(u => u.NickName).NotEmpty().WithMessage("Nickname is required.");
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required.");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required.");
    }
}