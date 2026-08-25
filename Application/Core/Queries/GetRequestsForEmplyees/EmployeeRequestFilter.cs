using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.GetRequestsForEmplyees
{
    public class EmployeeRequestFilter
    {
        public RequestType? Type { get; set; }

        public RequestStatus? Status { get; set; }

        public ComplaintPriority? Priority { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? NationalId { get; set; }

        public string? Title { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

    }
}
