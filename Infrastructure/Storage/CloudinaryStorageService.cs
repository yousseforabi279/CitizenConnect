using Application.storage;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Storage;

public class CloudinaryStorageService : IFileStorageService
{
    private readonly Cloudinary _cloudinary;

    // The domain model only ever tags media as Image or Video (see
    // Domain.Deputy.MediaType) — there is no "document" feature — so
    // anything outside these content types is rejected outright rather
    // than silently accepted as a raw/arbitrary upload.
    private static readonly HashSet<string> AllowedContentTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg", "image/png", "image/gif", "image/webp",
        "video/mp4", "video/webm", "video/quicktime", "video/mpeg"
    };

    private const long MaxUploadSizeBytes = 50 * 1024 * 1024; // 50MB

    public CloudinaryStorageService(IConfiguration config)
    {
        var cloudName = config["Cloudinary:CloudName"];
        var apiKey = config["Cloudinary:ApiKey"];
        var apiSecret = config["Cloudinary:ApiSecret"];

        if (string.IsNullOrWhiteSpace(cloudName) ||
            string.IsNullOrWhiteSpace(apiKey) ||
            string.IsNullOrWhiteSpace(apiSecret))
        {
            throw new InvalidOperationException(
                "Cloudinary configuration is missing.");
        }

        var account = new Account(
            cloudName,
            apiKey,
            apiSecret);

        _cloudinary = new Cloudinary(account);

        // Always generate HTTPS URLs
        _cloudinary.Api.Secure = true;
    }

    public async Task<BlobUploadResult> UploadFileAsync(
        FileUploadRequest file,
        string folderName)
    {
        if (file.Content == null)
        {
            throw new ArgumentNullException(
                nameof(file.Content));
        }

        if (string.IsNullOrWhiteSpace(file.FileName))
        {
            throw new ArgumentException(
                "File name is required.",
                nameof(file.FileName));
        }

        if (string.IsNullOrWhiteSpace(file.ContentType))
        {
            throw new ArgumentException(
                "Content type is required.",
                nameof(file.ContentType));
        }

        if (!AllowedContentTypes.Contains(file.ContentType))
        {
            throw new ArgumentException(
                $"File type '{file.ContentType}' is not allowed. Only image and video uploads are supported.",
                nameof(file.ContentType));
        }

        if (file.Length > MaxUploadSizeBytes)
        {
            throw new ArgumentException(
                $"File exceeds the maximum allowed size of {MaxUploadSizeBytes / (1024 * 1024)}MB.",
                nameof(file.Length));
        }

        var publicId =
            $"{folderName}/{Guid.NewGuid()}";

        var stream = file.Content;

        if (file.ContentType.StartsWith(
                "image/",
                StringComparison.OrdinalIgnoreCase))
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(
                    file.FileName,
                    stream),

                PublicId = publicId
            };

            var result =
                await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
            {
                throw new Exception(
                    $"Cloudinary upload failed: {result.Error.Message}");
            }

            return new BlobUploadResult
            {
                BlobName = result.PublicId,
                ContentType = file.ContentType,
                SizeBytes = file.Length
            };
        }

        if (file.ContentType.StartsWith(
                "video/",
                StringComparison.OrdinalIgnoreCase))
        {
            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(
                    file.FileName,
                    stream),

                PublicId = publicId
            };

            var result =
                await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
            {
                throw new Exception(
                    $"Cloudinary upload failed: {result.Error.Message}");
            }

            return new BlobUploadResult
            {
                BlobName = result.PublicId,
                ContentType = file.ContentType,
                SizeBytes = file.Length
            };
        }

        // PDF, Word, Excel, etc.
        var rawUploadParams = new RawUploadParams
        {
            File = new FileDescription(
                file.FileName,
                stream),

            PublicId = publicId
        };

        var rawResult =
            await _cloudinary.UploadAsync(rawUploadParams);

        if (rawResult.Error != null)
        {
            throw new Exception(
                $"Cloudinary upload failed: {rawResult.Error.Message}");
        }

        return new BlobUploadResult
        {
            BlobName = rawResult.PublicId,
            ContentType = file.ContentType,
            SizeBytes = file.Length
        };
    }

    public async Task DeleteFileAsync(
        string fileKey,
        string folderName)
    {
        if (string.IsNullOrWhiteSpace(fileKey))
            return;

        var deletionParams =
            new DeletionParams(fileKey);

        var result =
            await _cloudinary.DestroyAsync(
                deletionParams);

        if (result.Error != null)
        {
            throw new Exception(
                $"Cloudinary delete failed: {result.Error.Message}");
        }
    }

    public string GetFileUrl(
        string fileKey,
        string folderName)
    {
        if (string.IsNullOrWhiteSpace(fileKey))
            return string.Empty;

        return _cloudinary.Api.Url
            .Secure(true)
            .BuildUrl(fileKey);
    }
}