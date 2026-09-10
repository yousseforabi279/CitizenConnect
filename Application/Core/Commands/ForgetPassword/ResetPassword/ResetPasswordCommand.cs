using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.ForgetPassword.ResetPassword
{
    public record ResetPasswordCommand(
     string Email,
     string Code,
     string NewPassword,
     string ConfirmNewPassword
 ) : IRequest<Result<string>>;
}
