namespace DeputyProject.Requests.Employee
{
    public class UpdateEmployeeRequest
    {
        public string? About { get; set; }
        public IFormFile? Image { get; set; }
        public int DepartmentId { get; set; }
        public List<int>? OrganizationIds { get; set; }
    }
}
