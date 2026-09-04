namespace DeputyProject.Requests.AreaOfWork
{
    public class CreateAreaOfWorkRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
