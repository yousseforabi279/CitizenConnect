using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ComplaintResolution
    {
        public int Id { get; set; }

        public int ComplaintId { get; set; }

        public Complaint Complaint { get; set; } = null!;

        public int ResolvedByEmployeeId { get; set; }

        public Employee ResolvedByEmployee { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime ResolvedAt { get; set; }
    }
}
