using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CitizinRequiermentEmployee
    {
        public int id { get; set; }
        public int CitizinRequiermentId { get; set; }

        public int EmployeeId { get; set; }

        public CitizinRequierment CitizinRequierment { get; set; } = null!;

        public Employee Employee { get; set; } = null!;
    }
}
    