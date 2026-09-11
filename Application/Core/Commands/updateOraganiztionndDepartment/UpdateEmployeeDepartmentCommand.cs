using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.updateOraganiztionndDepartment
{
    public record UpdateEmployeeDepartmentCommand(
           int EmployeeId,
           int DepartmentId,
           IReadOnlyCollection<int> OrganizationIds
       ) : IRequest<Result<int>>;
}
