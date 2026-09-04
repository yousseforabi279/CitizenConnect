namespace DeputyProject.Requests.Achievement
{
    public class CreateAchievementRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Media { get; set; }
    }
}
