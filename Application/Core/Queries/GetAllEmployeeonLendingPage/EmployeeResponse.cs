using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.GetAllEmployeeonLendingPage
{
    public class EmployeeResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public string? about { get; set; }
        public string? phone { get; set; }
    }
}
