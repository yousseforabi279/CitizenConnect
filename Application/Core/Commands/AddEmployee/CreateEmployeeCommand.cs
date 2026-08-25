using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.AddEmployee
{
    public record CreateEmployeeCommand(
     string FullName,
     string Email,
     string Password,
     string Role,
     int DepartmentId
 ) : IRequest<Result<int>>;
}
