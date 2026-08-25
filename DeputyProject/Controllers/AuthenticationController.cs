using Application.Core.Commands.AddEmployee;
using Application.Core.Commands.CreateCompliant;
using Application.Core.Commands.Login;
using Bank.Api.Controllers;
using DeputyProject.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IMediator _mediator) : base(_mediator) { }

        [HttpPost(ApiRoutes.Authentication.Login)]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpPost(ApiRoutes.Authentication.Register)]
        public async Task<IActionResult> Register(CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

    }
}
