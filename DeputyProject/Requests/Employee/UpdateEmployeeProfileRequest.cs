namespace DeputyProject.Requests.Employee
{
    public class UpdateEmployeeProfileRequest
    {
        public string? About { get; set; }
        public string? Phone { get; set; }
        public IFormFile? Image { get; set; }
    }
}
