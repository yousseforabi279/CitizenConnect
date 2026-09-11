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
        string? About,
        string? Phone,
        FileUploadRequest? Image
    ) : IRequest<Result<int>>;
}
