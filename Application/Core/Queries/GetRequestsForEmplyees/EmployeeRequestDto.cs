using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.GetRequestsForEmplyees
{
    public class EmployeeRequestDto
    {
       
            public int Id { get; set; }

            public string Type { get; set; } = null!;

            public string Title { get; set; } = null!;

            public string Content { get; set; } = null!;

            public string CitizenName { get; set; } = null!;

            public string NationalId { get; set; } = null!;

            public string Phone { get; set; } = null!;

            public string Priority { get; set; } = null!;

            public string Status { get; set; } = null!;

            public DateTime CreatedAt { get; set; }
        
    }
}
