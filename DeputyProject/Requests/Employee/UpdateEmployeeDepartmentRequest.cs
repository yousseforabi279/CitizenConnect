namespace DeputyProject.Requests.Employee
{
    public class UpdateEmployeeDepartmentRequest
    {
        public int DepartmentId { get; set; }
        public IReadOnlyCollection<int> OrganizationIds { get; set; } = new List<int>();
    }
}
