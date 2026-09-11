using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Application.Core.Queries.GetRequestsForEmplyees
{
    public class EmployeeRequestDto
    {
        public int Id { get; set; }

        public CitizenInfoDto Citizen { get; set; } = null!;

        public RequestInfoDto Request { get; set; } = null!;

        public MediaInfoDto? Media { get; set; }

        public List<CommentDto> Comments { get; set; } = new();
    }

    public class CitizenInfoDto
    {
        public string NationalId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }

    public class RequestInfoDto
    {
        public RequestType Type { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public RequestStatus Status { get; set; }
        public ComplaintPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class MediaInfoDto
    {
        public string? BlobName { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public string? MediaUrl { get; set; }
    }

    public class CommentDto
    {
        public int Id { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
    }
}