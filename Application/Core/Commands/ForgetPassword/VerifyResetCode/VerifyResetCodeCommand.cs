using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.ForgetPassword.NewFolder
{
    public record VerifyResetCodeCommand(
       string Email,
       string Code
   ) : IRequest<Result<string>>;
}
