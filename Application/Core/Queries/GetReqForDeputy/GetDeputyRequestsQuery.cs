using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.Core.Queries.GetRequestsForDeputy
{
    public record GetDeputyRequestsQuery(
        int PageNumber = 1,
        int PageSize = 10,
        RequestType? Type = null,
        RequestStatus? Status = null,
        ComplaintPriority? Priority = null,
        string? Name = null,
        string? Phone = null,
        string? NationalId = null,
        string? Title = null,
        int? DepartmentId = null,
        int? OrganizationId = null
    ) : IRequest<Result<PaginatedResult<DeputyRequestDto>>>;
}