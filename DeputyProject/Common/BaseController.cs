using Application.Common;
using DeputyProject.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    protected BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        var response = new ResultResponse<T>
        {
            IsSuccess = result.IsSuccess,

            Status = result.Status switch
            {
                ResultStatus.Success => 200,
                ResultStatus.ValidationError => 422,
                ResultStatus.BadRequest => 400,
                ResultStatus.NotFound => 404,
                ResultStatus.Conflict => 409,
                ResultStatus.Unauthorized => 401,
                ResultStatus.Forbidden => 403,
                ResultStatus.RequiresTwoFactor => 403,
                ResultStatus.Failure => 500,
                ResultStatus.InternalServerError => 500,

                _ => 500
            },

            Error = result.Error,
            Value = result.Value,
            Message = result.Message
        };

        return result.Status switch
        {
            ResultStatus.Success =>
                Ok(response),

            ResultStatus.ValidationError =>
                UnprocessableEntity(response),

            ResultStatus.BadRequest =>
                BadRequest(response),

            ResultStatus.NotFound =>
                NotFound(response),

            ResultStatus.Conflict =>
                Conflict(response),

            ResultStatus.Unauthorized =>
                Unauthorized(response),

            ResultStatus.Forbidden =>
                StatusCode(403, response),

            ResultStatus.RequiresTwoFactor =>
                StatusCode(403, response),

            ResultStatus.Failure =>
                StatusCode(500, response),

            ResultStatus.InternalServerError =>
                StatusCode(500, response),

            _ =>
                StatusCode(500, response)
        };
    }
}