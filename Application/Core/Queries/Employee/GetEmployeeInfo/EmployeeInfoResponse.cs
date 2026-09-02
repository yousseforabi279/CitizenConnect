using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Employee.GetEmployeeInfo
{
    public class EmployeeInfoResponse
    {
        public string Name { get; set; } = null!;
        public string Department { get; set; } = null!;
    }
}
