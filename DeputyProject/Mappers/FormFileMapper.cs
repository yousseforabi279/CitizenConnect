using Application.storage;

namespace DeputyProject.Mappers
{
    public static class FormFileMapper
    {
        public static FileUploadRequest MapToFileUploadRequest(this IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            return new FileUploadRequest
            {
                Content = file.OpenReadStream(),
                FileName = file.FileName,
                ContentType = file.ContentType,
                Length = file.Length
            };
        }
    }
}
