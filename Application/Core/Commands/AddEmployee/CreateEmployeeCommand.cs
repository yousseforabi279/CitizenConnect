using Application.Common;
using Application.storage;
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
     int DepartmentId,
     int OrganizationId,
     string PhoneNumber,
     string? About,
     FileUploadRequest? Image
 ) : IRequest<Result<int>>;
}
