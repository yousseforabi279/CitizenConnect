using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Organization
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<EmployeeOrganizations> EmployeeOrganizations { get; set; }
            = new List<EmployeeOrganizations>();
    }
}
