using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CitizenRequirementContent
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CitizenRequirementId { get; set; }

        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public CitizenRequirement CitizenRequirement { get; set; } = null!;

        public Employee Employee { get; set; } = null!;

    }
}
