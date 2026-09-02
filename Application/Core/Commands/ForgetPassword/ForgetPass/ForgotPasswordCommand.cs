using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.ForgetPassword.ForgetPass
{
    public record ForgotPasswordCommand(string Email) : IRequest<Result<string>>;
}
