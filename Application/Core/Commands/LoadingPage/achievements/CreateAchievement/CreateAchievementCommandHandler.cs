using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.achievements;
using Application.storage;
using Domain.Deputy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.achievements.CreateAchievement
{
    internal class CreateAchievementCommandHandler
    : IRequestHandler<CreateAchievementCommand, Result<AchievementDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _blobStorageService;
        private const string FolderName = "achievement-files";

        public CreateAchievementCommandHandler(IUnitOfWork unitOfWork, IFileStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<AchievementDto>> Handle(
            CreateAchievementCommand request,
            CancellationToken cancellationToken)
        {
            var achievement = new Achievement
            {
                Title = request.Title,
                Description = request.Description,
            };
            if (request.Media != null)
            {
                var upload = await _blobStorageService.UploadFileAsync(
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

                // Cloudinary URL
                achievement.MediaUrl =
                    _blobStorageService.GetFileUrl(
                        achievement.BlobName,
                        FolderName);
            }

            await _unitOfWork.Achievement.AddAsync(achievement);

            await _unitOfWork.SaveChangesAsync();

            return Result<AchievementDto>.Success(
            new AchievementDto
            {
                Id = achievement.Id,
                Title = achievement.Title,
                Description = achievement.Description,

                MediaUrl = achievement.BlobName != null
                    ? _blobStorageService.GetFileUrl(
                        achievement.BlobName,
                        FolderName)
                    : null,

                ContentType = achievement.ContentType,
                MediaType = achievement.MediaType
          
            },
            "تم إضافة الإنجاز بنجاح.");
        }
    }

}
