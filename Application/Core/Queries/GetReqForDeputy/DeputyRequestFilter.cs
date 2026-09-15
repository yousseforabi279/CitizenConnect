using Domain.Enums;

namespace Application.Core.Queries.GetRequestsForDeputy
{
    public class DeputyRequestFilter
    {
        public RequestType? Type { get; set; }
        public RequestStatus? Status { get; set; }
        public ComplaintPriority? Priority { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? NationalId { get; set; }
        public string? Title { get; set; }
        public int? DepartmentId { get; set; }
        public int? OrganizationId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}