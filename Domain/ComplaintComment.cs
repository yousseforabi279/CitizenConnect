using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ComplaintComment
    {
        public int Id { get; set; }

        public int ComplaintId { get; set; }

        public Complaint Complaint { get; set; } = null!;

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
