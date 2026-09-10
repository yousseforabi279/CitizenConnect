using Application.Common;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.UpdateRequestStatus
{
    public class UpdateRequestCommand : IRequest<Result<string>>
    {
        public int CitizenRequirementId { get; set; }
        public RequestStatus? Status { get; set; }
        public RequestPriority? Priority { get; set; }
        public string? Comment { get; set; }
    }
}
