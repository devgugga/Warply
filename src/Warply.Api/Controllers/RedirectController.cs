using Microsoft.AspNetCore.Mvc;
using Warply.Application.UseCases.Redirect.Url;

namespace Warply.Api.Controllers;

[Route("/")]
[ApiController]
public class RedirectController : ControllerBase
{
    [HttpGet("{shortCode}")]
    public async Task<IActionResult> RedirectUser(string shortCode, [FromServices] IRedirectUseCase useCase)
    {
        var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                 HttpContext.Connection.RemoteIpAddress?.ToString();

        Console.WriteLine(ip);

        var redirectUrl = await useCase.Redirect(shortCode);

        return new RedirectResult(redirectUrl.OriginalUrl);
    }
}