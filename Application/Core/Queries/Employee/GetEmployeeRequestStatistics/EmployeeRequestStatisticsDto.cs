using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Employee.GetEmployeeRequestStatistics
{
    public class EmployeeRequestStatisticsDto
    {
        public int Total { get; set; }
        public int New { get; set; }
        public int InProgress { get; set; }
        public int Completed { get; set; }
    }
}
