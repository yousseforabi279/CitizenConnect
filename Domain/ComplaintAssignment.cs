using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ComplaintAssignment
    {
        public int Id { get; set; }
        public int ComplaintId { get; set; }
        public Complaint Complaint { get; set; } = null!;
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!; // We Redendent it Cz maybe the Emp change its Department   
        public DateTime AssignedAt { get; set; }
        public DateTime? UnassignedAt { get; set; }
        public string? Note { get; set; }
    }
}
