using Domain.Deputy;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CitizenRequirement
    {

        public int Id { get; set; }
        [ForeignKey(nameof(Citizen))]
        public string CitizenNationalId { get; set; } = null!;
        public int? DepartmentId { get; set; }
        public RequestType Type { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Citizen Citizen { get; set; } = null!;
        public Department? Department { get; set; }
        public RequestPriority Priority { get; set; }
        public ICollection<CitizenRequirementContent> Comments { get; set; }
             = new List<CitizenRequirementContent>();
        public ICollection<CitizenRequirementEmployee> Employees { get; set; }
        = new List<CitizenRequirementEmployee>();



        // Media metadata (replaces raw Video_image string)
        public string BlobName { get; set; }
        public string MediaFileName { get; set; }
        public string ContentType { get; set; }
        public long FileSizeBytes { get; set; }
        public MediaType MediaType { get; set; }
        public DateTime UploadedAt { get; set; }
        public string MediaUrl { get; set; }        // full blob URL

    }
}
