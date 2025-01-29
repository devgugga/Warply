using FluentValidation;
using Warply.Communication.Request.Url;

namespace Warply.Application.UseCases.Register.Url;

internal class RegisterUrlValidator : AbstractValidator<RequestRegisterUrlJson>
{
    public RegisterUrlValidator()
    {
        RuleFor(url => url.OriginalUrl).NotEmpty().WithMessage("Original Url is required.");
    }
}