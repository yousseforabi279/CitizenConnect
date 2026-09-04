namespace DeputyProject.Requests.MotionsForInformation
{
    public class UpdateMotionsForInformationRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Media { get; set; }
    }
}
