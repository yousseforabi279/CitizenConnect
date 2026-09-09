using Domain.Deputy;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.CitizenRequests.GetRequestById
{
    public class CitizenRequestDto
    {
        public int Id { get; set; }

        // Citizen
        public string CitizenNationalId { get; set; } = null!;
        public string CitizenFullName { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
        public string Phone { get; set; } = null!;

        // Request
        public RequestType Type { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public RequestStatus Status { get; set; }
        public ComplaintPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }

        // Media
        public string? MediaFileName { get; set; }
        public string? ContentType { get; set; }
        public long FileSizeBytes { get; set; }
        public MediaType? MediaType { get; set; }
        public DateTime? UploadedAt { get; set; }
        public string? MediaUrl { get; set; }
    }
}
