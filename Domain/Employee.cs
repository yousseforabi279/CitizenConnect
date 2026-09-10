using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Employee
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        [MaxLength(450)]
        public string UserId { get; set; }

        public User User { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<CitizenRequirementEmployee> Requests { get; set; }
              = new List<CitizenRequirementEmployee>();
        public ICollection<EmployeeOrganizations> EmployeeOrganizations { get; set; }
    = new List<EmployeeOrganizations>();
        [MaxLength(2000)]
        public string? About { get; set; }
    }
}
