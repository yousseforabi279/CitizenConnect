using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class EmployeeOrganizations
    {
        public int id { get; set; }
        public int EmployeeId { get; set; }

        public int OrganizationId { get; set; }

        public Employee Employee { get; set; } = null!;

        public Organization Organization { get; set; } = null!;
    }
}
