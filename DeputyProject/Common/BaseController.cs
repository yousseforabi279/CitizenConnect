using Application.Common;
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
        return result.Status switch
        {
            ResultStatus.Success =>
                Ok(result),

            ResultStatus.BadRequest =>
                BadRequest(result),

            ResultStatus.NotFound =>
                NotFound(result),

            ResultStatus.Conflict =>
                Conflict(result),

            ResultStatus.Unauthorized =>
                Unauthorized(result),

            ResultStatus.Forbidden =>
                Forbid(),

            _ =>
                StatusCode(500, result)
        };
    }
}