using Microsoft.AspNetCore.Mvc;
using Warply.Application.UseCases.Register.Auth;
using Warply.Communication.Request.Auth;

namespace Warply.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromServices] IJwtTokenUseCase useCase,
        [FromBody] RequestUserLoginJson request)
    {
        var response = await useCase.Login(request);

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromServices] IJwtTokenUseCase useCase,
        [FromBody] RequestUserRefreshTokenJson request)
    {
        var response = await useCase.RefreshToken(request);

        return Ok(response);
    }
}