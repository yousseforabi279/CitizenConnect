namespace DeputyProject.Requests.DeputyWord
{

    public class UpdateDeputyWordsRequest
    {
        public string? Title { get; set; }
        public IFormFile? Media { get; set; }
    }
}
