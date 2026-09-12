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
  public record UpdateEmployeeProfileCommand(
        int? EmployeeId,
        string? FullName,
        string? About,
        string? Phone,
        int? DepartmentId,
        List<int>? OrganizationIds,
        FileUploadRequest? Image
    ) : IRequest<Result<int>>;
}
