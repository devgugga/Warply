using Microsoft.AspNetCore.Mvc;
using Warply.Application.UseCases.Users;
using Warply.Communication.Request.User;

namespace Warply.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Created(string.Empty, response);
    }
}