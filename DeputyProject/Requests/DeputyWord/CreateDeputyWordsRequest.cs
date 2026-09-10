namespace DeputyProject.Requests.DeputyWord
{
    public class CreateDeputyWordsRequest
    {
        public string? Title { get; set; }
        public IFormFile Media { get; set; }
    }
}
