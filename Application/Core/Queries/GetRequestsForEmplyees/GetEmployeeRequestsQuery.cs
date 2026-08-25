using Application.Common;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.GetRequestsForEmplyees
{
    public record GetEmployeeRequestsQuery(
        int PageNumber = 1,
        int PageSize = 10,
        RequestType? Type = null,
        RequestStatus? Status = null,
        ComplaintPriority? Priority = null
    ) : IRequest<Result<PaginatedResult<EmployeeRequestDto>>>;
}
