using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CitizinRequierment
    {

        public int Id { get; set; }
        [ForeignKey(nameof(Citizen))]
        public string CitizenNationalId { get; set; } = null!;
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public RequestType Type { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Comment { get; set; }

        public Citizen Citizen { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public Department Department { get; set; } = null!;
        public ICollection<CitizinRequiermentContent> Comments { get; set; }
             = new List<CitizinRequiermentContent>();

    }
}
