using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.achievements;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.Achievement.GetAchievementById
{
    internal class GetAchievementQueryHandler
     : IRequestHandler<
         GetAchievementQuery,
         Result<AchievementDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "achievement-files";

        public GetAchievementQueryHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;

        }

        public async Task<Result<AchievementDto>> Handle(
            GetAchievementQuery request,
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

            var response = new AchievementDto
            {
                Id = achievement.Id,
                Title = achievement.Title,
                Description = achievement.Description,
                MediaUrl = achievement.BlobName != null ? _blobStorageService.GetReadSasUrl(achievement.BlobName, ContainerName) : null,
                ContentType = achievement.ContentType,
                MediaType = achievement.MediaType
            };

            return Result<AchievementDto>.Success(
                response,
                "تم جلب بيانات الإنجاز بنجاح.");
        }
    }
}
