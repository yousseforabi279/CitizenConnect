using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.achievements;
using Application.storage;
using Domain.Deputy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.achievements.EditAchievement
{
    internal class UpdateAchievementCommandHandler
     : IRequestHandler<UpdateAchievementCommand, Result<AchievementDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "achievement-files";

        public UpdateAchievementCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;

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
            if (request.Media != null)
            {
                if (!string.IsNullOrEmpty(achievement.BlobName))
                    await _blobStorageService.DeleteFileAsync(achievement.BlobName, ContainerName);

                var upload = await _blobStorageService.UploadFileAsync(request.Media, ContainerName);

                achievement.BlobName = upload.BlobName;
                achievement.MediaFileName = request.Media.FileName;
                achievement.ContentType = upload.ContentType;
                achievement.FileSizeBytes = upload.SizeBytes;
                achievement.MediaType = request.Media.ContentType.StartsWith("video") ? MediaType.Video : MediaType.Image;
                achievement.UploadedAt = DateTime.UtcNow;
                achievement.MediaUrl = achievement.BlobName != null ? _blobStorageService.GetReadSasUrl(achievement.BlobName,ContainerName) : null;

            }
            achievement.Title = request.Title;
            achievement.Description = request.Description;

            await _unitOfWork.SaveChangesAsync();

            return Result<AchievementDto>.Success(
                new AchievementDto
                {
                    Id = achievement.Id,
                    Title = achievement.Title,
                    Description = achievement.Description,
                    MediaUrl = achievement.BlobName != null ? _blobStorageService.GetReadSasUrl(achievement.BlobName,ContainerName) : null,
                    ContentType = achievement.ContentType,
                    MediaType = achievement.MediaType
                },
                "تم تعديل الإنجاز بنجاح.");
        }
    }
}
