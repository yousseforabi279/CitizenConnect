using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.GetAllEmployeeonLendingPage
{
    public class EmployeeResponse {
        public int Id { get; set; }
        public string Name { get; set; } = null!; 
        public string? Phone { get; set; } 
        
        public string? about { get; set; }
    }
}
