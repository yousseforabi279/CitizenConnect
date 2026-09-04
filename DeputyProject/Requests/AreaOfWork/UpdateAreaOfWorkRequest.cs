namespace DeputyProject.Requests.AreaOfWork
{
    public class UpdateAreaOfWorkRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; } // optional on update
    }
}
