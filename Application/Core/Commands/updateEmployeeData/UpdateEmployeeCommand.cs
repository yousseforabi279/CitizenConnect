using Application.Common;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.updateEmployee
{
    public record UpdateEmployeeCommand(
           int EmployeeId,
           string? About,
           FileUploadRequest? Image,
           int DepartmentId,
           IReadOnlyCollection<int> OrganizationIds
       ) : IRequest<Result<int>>;
}
