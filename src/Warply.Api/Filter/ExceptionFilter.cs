using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Warply.Communication.Response.Error;
using Warply.Exception.BaseExceptions;
using Warply.Exception.Exceptions;

namespace Warply.Api.Filter;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is WarplyException) HandleProjectException(context);
        else ThrowUnknownError(context);
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ErrorOnValidationException exception:
            {
                var errorResponse = new ResponseErrorJson(exception.Errors);

                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse);
                break;
            }
            case EmailAlreadyExistsException emailException:
            {
                var errorResponse = new ResponseErrorJson($"Email {emailException.Email} already exists.");

                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Result = new ConflictObjectResult(errorResponse);
                break;
            }
            case NotFoundException notFoundException:
            {
                var errorResponse = new ResponseErrorJson(notFoundException.Message);

                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Result = new NotFoundObjectResult(errorResponse);
                break;
            }
            case UploadException uploadException:
            {
                var errorResponse = new ResponseErrorJson(uploadException.Message);

                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse);
                break;
            }
            case InvalidException invalidException:
            {
                var errorResponse = new ResponseErrorJson(invalidException.Message);

                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse);
                break;
            }
            default:
            {
                var errorResponse = new ResponseErrorJson(context.Exception.Message);

                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse);
                break;
            }
        }
    }

    private static void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson("Unknown Error");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}