using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Employee
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId{ get; set; }

        public User User { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<ComplaintAssignment> ComplaintAssignments { get; set; }
            = new List<ComplaintAssignment>();
    }
}
