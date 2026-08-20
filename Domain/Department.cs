using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; }
            = new List<Employee>();

        public ICollection<ComplaintAssignment> ComplaintAssignments { get; set; }
            = new List<ComplaintAssignment>();
    }   
}
