using FluentValidation;
using Warply.Communication.Request.Auth;

namespace Warply.Application.UseCases.Register.Auth;

internal abstract class TokenValidatorBase<T> : AbstractValidator<T>
{
}

internal class JwtTokenValidator : TokenValidatorBase<RequestUserLoginJson>
{
    public JwtTokenValidator()
    {
        RuleFor(token => token.Email).NotEmpty().WithMessage("Email is required.");
        RuleFor(token => token.Password).NotEmpty().WithMessage("Password is required.");
    }
}

internal class RefreshTokenValidator : TokenValidatorBase<RequestUserRefreshTokenJson>
{
    public RefreshTokenValidator()
    {
        RuleFor(token => token.AccessToken).NotEmpty().WithMessage("AccessToken is required.");
        RuleFor(token => token.RefreshToken).NotEmpty().WithMessage("Refresh token is required.");
    }
}