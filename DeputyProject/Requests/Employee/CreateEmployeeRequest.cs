namespace DeputyProject.Requests.Employee
{
    public class CreateEmployeeRequest
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int DepartmentId { get; set; }
        public int OrganizationId { get; set; }
        public string? About { get; set; }
        public IFormFile? Image { get; set; }
    }
}
