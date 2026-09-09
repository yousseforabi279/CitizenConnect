using Domain.Enums;

namespace DeputyProject.Requests.CitizenRequests
{
    public class CreateCitizenRequest
    {
        public RequestType RequestType { get; set; }

        // Citizen information
        public string NationalId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
        public string Phone { get; set; } = null!;

        // Complaint information
        public string? Title { get; set; }
        public string? Description { get; set; }

        public int DepartmentId { get; set; }
        public int OrganizationId { get; set; }
        public IFormFile Image { get; set; }

    }
}
