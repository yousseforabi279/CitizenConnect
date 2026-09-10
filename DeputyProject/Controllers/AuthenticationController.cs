using Application.Core.Commands.AddEmployee;
using Application.Core.Commands.ChangePassword;
using Application.Core.Commands.ForgetPassword.ForgetPass;
using Application.Core.Commands.ForgetPassword.VerifyResetCode;
using Application.Core.Commands.ForgetPassword.ResetPassword;
using Application.Core.Commands.Login;
using Application.Core.Commands.RefreshAccessToken;
using DeputyProject.Controllers;
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

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.Login)]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        // Only an existing employee can onboard another one — self-registration
        // as staff is not exposed. The very first employee account must be
        // provisioned out-of-band (e.g. seeded directly).
        [Authorize(Roles = "Employee")]
        [HttpPost(ApiRoutes.Authentication.Register)]
        public async Task<IActionResult> Register(CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.refreshtoken)]
        public async Task<IActionResult> RefreshToken(RefreshAccessTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [Authorize(Roles = "Employee")]
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
