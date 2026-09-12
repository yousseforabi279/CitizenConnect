using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.achievements;
using Application.storage;
using Domain.Deputy;
using MediatR;

namespace Application.Core.Commands.Deputy.achievements.EditAchievement
{
    internal class UpdateAchievementCommandHandler
        : IRequestHandler<UpdateAchievementCommand, Result<AchievementDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "achievement-files";

        public UpdateAchievementCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<AchievementDto>> Handle(
            UpdateAchievementCommand request,
            CancellationToken cancellationToken)
        {
            var achievement = await _unitOfWork.Achievement
                .GetByIdAsync(request.AchievementId);

            if (achievement is null)
            {
                return Result<AchievementDto>.Failure(
                    ResultStatus.NotFound,
                    "الإنجاز غير موجود.");
            }

            // If a new media file was uploaded
            if (request.Media != null)
            {
                // Delete old file from Cloudinary
                if (!string.IsNullOrEmpty(achievement.BlobName))
                {
                    await _fileStorageService.DeleteFileAsync(
                        achievement.BlobName,
                        FolderName);
                }

                // Upload new file
                var upload = await _fileStorageService.UploadFileAsync(
                    request.Media,
                    FolderName);

                achievement.BlobName = upload.BlobName;
                achievement.MediaFileName = request.Media.FileName;
                achievement.ContentType = upload.ContentType;
                achievement.FileSizeBytes = upload.SizeBytes;

                achievement.MediaType =
                    request.Media.ContentType.StartsWith("video/")
                        ? MediaType.Video
                        : MediaType.Image;

                achievement.UploadedAt = DateTime.UtcNow;

                // Generate Cloudinary URL
                achievement.MediaUrl =
                    _fileStorageService.GetFileUrl(
                        achievement.BlobName,
                        FolderName, achievement.ContentType);
            }

            achievement.Title = request.Title;
            achievement.Description = request.Description;

            await _unitOfWork.SaveChangesAsync();

            var response = new AchievementDto
            {
                Id = achievement.Id,
                Title = achievement.Title,
                Description = achievement.Description,

                MediaUrl = !string.IsNullOrEmpty(achievement.BlobName)
                    ? _fileStorageService.GetFileUrl(
                        achievement.BlobName,
                        FolderName, achievement.ContentType)
                    : null,

                ContentType = achievement.ContentType,
                MediaType = achievement.MediaType
            };

            return Result<AchievementDto>.Success(
                response,
                "تم تعديل الإنجاز بنجاح.");
        }
    }
}