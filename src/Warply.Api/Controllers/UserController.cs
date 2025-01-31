using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warply.Application.UseCases.Recive.User;
using Warply.Application.UseCases.Register.User;
using Warply.Application.UseCases.Update.User;
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

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUser([FromServices] IReciveUserUseCase useCase)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var response = await useCase.ExecuteAsync(userId);

        return Ok(response);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> Update([FromServices] IUpdateUserUseCase useCase,
        [FromBody] RequestUpdateUserJson request)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var response = await useCase.Update(request, userId);

        return Ok(response);
    }

    [HttpPut("upload-image")]
    [Authorize]
    public async Task<IActionResult> OploadImage([FromForm] IFormFile? image, [FromServices] IUpdateUserUseCase useCase)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        if (image is null || image.Length is 0)
            return BadRequest("No image file provided");

        using var memoryStream = new MemoryStream();
        await image.CopyToAsync(memoryStream);
        var imageBytes = memoryStream.ToArray();

        var response = await useCase.Upload(imageBytes, image.FileName, userId);

        return Ok(response);
    }
}