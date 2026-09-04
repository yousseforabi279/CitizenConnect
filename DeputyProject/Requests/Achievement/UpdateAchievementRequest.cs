namespace DeputyProject.Requests.Achievement
{
    public class UpdateAchievementRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Media { get; set; }
    }
}
