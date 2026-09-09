using Application.Common;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.NewFolder
{
    public class ChangeRequestStatusCommand : IRequest<Result<string>>
    {
        public int CitizinRequiermentId { get; set; }
        public RequestStatus? Status { get; set; }
        public ComplaintPriority? Priority { get; set; }
    }
}
