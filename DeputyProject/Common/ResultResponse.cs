namespace DeputyProject.Common
{
    public class ResultResponse<T>
    {
        public bool IsSuccess { get; set; }

        public int Status { get; set; }

        public string? Error { get; set; }

        public T? Value { get; set; }

        public string? Message { get; set; }
    }
}
