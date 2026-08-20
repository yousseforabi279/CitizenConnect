using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ComplaintStatusHistory
    {
        public int Id { get; set; }

        public int ComplaintId { get; set; }

        public Complaint Complaint { get; set; } = null!;

        public ComplaintStatus OldStatus { get; set; }

        public ComplaintStatus NewStatus { get; set; }

        public int ChangedByEmployeeId { get; set; }

        public Employee ChangedByEmployee { get; set; } = null!;

        public string? Note { get; set; }

        public DateTime ChangedAt { get; set; }
    }
}
