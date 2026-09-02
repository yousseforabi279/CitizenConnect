using Application.Core.Commands.AddEmployee;
using Application.Core.Commands.ChangePassword;
using Application.Core.Commands.CreateCompliant;
using Application.Core.Commands.ForgetPassword.ForgetPass;
using Application.Core.Commands.ForgetPassword.NewFolder;
using Application.Core.Commands.ForgetPassword.Resetpassword;
using Application.Core.Commands.Login;
using Bank.Api.Controllers;
using DeputyProject.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpPost(ApiRoutes.Authentication.ChangePassword)]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.forgotpassword)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.verifyresetcode)]
        public async Task<IActionResult> VerifyResetCode([FromBody] VerifyResetCodeCommand command)
        { 
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.resetpassword)] 
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        { 
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

    }
}
