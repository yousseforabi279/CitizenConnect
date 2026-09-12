namespace DeputyProject.Requests.Employee
{
    public class UpdateEmployeeProfileRequest
    {
        public int? EmployeeId { get; set; }
        public string? fullname { get; set; }
        public string? About { get; set; }
        public string? Phone { get; set; }
        public int? DepartmentId { get; set; }
        public List<int>? OrganizationIds { get; set; }
        public IFormFile? Image { get; set; }
    }
}
