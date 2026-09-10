using Domain.Deputy;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(14)]
        public string CitizenNationalId { get; set; } = null!;
        public int? DepartmentId { get; set; }
        public RequestType Type { get; set; }

        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(4000)]
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
        [MaxLength(300)]
        public string BlobName { get; set; }

        [MaxLength(300)]
        public string MediaFileName { get; set; }

        [MaxLength(100)]
        public string ContentType { get; set; }
        public long FileSizeBytes { get; set; }
        public MediaType MediaType { get; set; }
        public DateTime UploadedAt { get; set; }

        [MaxLength(500)]
        public string MediaUrl { get; set; }        // full blob URL

    }
}
