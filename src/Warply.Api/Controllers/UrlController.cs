using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warply.Application.UseCases.Register.Url;
using Warply.Communication.Request.Url;

namespace Warply.Api.Controllers;

[Route("api/url")]
[ApiController]
public class UrlController : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Register([FromServices] IRegisterUrlUseCase useCase,
        [FromBody] RequestRegisterUrlJson request)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var response = await useCase.ExecuteAsync(request, userId);

        return Created(string.Empty, response);
    }
}