using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Login
{
    public record LoginCommand(
     string Email,
     string Password
 ) : IRequest<Result<LoginResponse>>;
}
