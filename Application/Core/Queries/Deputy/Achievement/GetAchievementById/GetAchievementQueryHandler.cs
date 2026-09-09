using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.achievements;
using Application.Core.Queries.Deputy.Achievement.GetAchievementById;
using Application.storage;
using MediatR;

namespace Application.Core.Queries.Deputy.Achievement.GetAchievementById
{
    internal class GetAchievementQueryHandler
        : IRequestHandler<
            GetAchievementQuery,
            Result<AchievementDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "achievement-files";

        public GetAchievementQueryHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<AchievementDto>> Handle(
            GetAchievementQuery request,
            CancellationToken cancellationToken)
        {
            var achievement =
                await _unitOfWork.Achievement
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

                MediaUrl = string.IsNullOrWhiteSpace(achievement.BlobName)
                    ? null
                    : _fileStorageService.GetFileUrl(
                        achievement.BlobName,
                        FolderName),

                ContentType = achievement.ContentType,
                MediaType = achievement.MediaType
            };

            return Result<AchievementDto>.Success(
                response,
                "تم جلب بيانات الإنجاز بنجاح.");
        }
    }
}