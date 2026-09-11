using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Employee.GetEmployeeInfo
{
    public class EmplyeeInfo
    {
        public string Name { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public List<string> Organizations { get; set; } = new();
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
