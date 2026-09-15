using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Application.Core.Queries.GetRequestsForDeputy
{
    public class DeputyRequestDto
    {
        public int Id { get; set; }

        public CitizenInfoDto Citizen { get; set; } = null!;

        public RequestInfoDto Request { get; set; } = null!;

        public MediaInfoDto? Media { get; set; }

        public List<AssignedEmployeeDto> AssignedEmployees { get; set; } = new();
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

    public class AssignedEmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
        public List<string> Organizations { get; set; } = new();
    }
}