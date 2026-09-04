using Application.Common;
using Application.Contracts;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.achievements.DeleteAchievement
{
    internal class DeleteAchievementCommandHandler
      : IRequestHandler<DeleteAchievementCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "achievement-files";

        public DeleteAchievementCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;

        }

        public async Task<Result<int>> Handle(
            DeleteAchievementCommand request,
            CancellationToken cancellationToken)
        {
            var achievement = await _unitOfWork.Achievement
                .GetByIdAsync(request.AchievementId);

            if (achievement is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "الإنجاز غير موجود.");
            }
            if (!string.IsNullOrEmpty(achievement.BlobName))
                await _blobStorageService.DeleteFileAsync(achievement.BlobName, ContainerName);

            _unitOfWork.Achievement.Delete(achievement);
            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                achievement.Id,
                "تم حذف الإنجاز بنجاح.");
        }
    }
}
