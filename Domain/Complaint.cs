using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Complaint
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string CitizenName { get; set; } = null!;

        public string NationalId { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public int CategoryId { get; set; }
        public ComplaintCategory Category { get; set; } = null!;

        public ComplaintStatus Status { get; set; }

        public ComplaintPriority Priority { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<ComplaintStatusHistory> StatusHistory { get;} = new List<ComplaintStatusHistory>();
        public ICollection<ComplaintComment> Comments { get; }= new List<ComplaintComment>();
        public ICollection<ComplaintAssignment> Assignments { get;} = new List<ComplaintAssignment>();
        public ComplaintResolution? Resolution { get; set; }
    }
}
