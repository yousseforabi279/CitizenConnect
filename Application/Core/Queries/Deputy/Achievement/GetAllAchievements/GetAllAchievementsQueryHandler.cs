using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.achievements;
using Application.Core.Queries.Deputy.Achievement.GetAchievementById;
using Application.storage;
using MediatR;

namespace Application.Core.Queries.Deputy.Achievement.GetAllAchievements
{
    internal class GetAllAchievementsQueryHandler
        : IRequestHandler<
            GetAllAchievementsQuery,
            Result<List<AchievementDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "achievement-files";

        public GetAllAchievementsQueryHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<List<AchievementDto>>> Handle(
            GetAllAchievementsQuery request,
            CancellationToken cancellationToken)
        {
            var achievements =
                await _unitOfWork.Achievement
                    .GetAllAsync();

            var response = achievements
                .Select(achievement => new AchievementDto
                {
                    Id = achievement.Id,
                    Title = achievement.Title,
                    Description = achievement.Description,

                    MediaUrl = string.IsNullOrWhiteSpace(
                        achievement.BlobName)
                        ? null
                        : _fileStorageService.GetFileUrl(
                            achievement.BlobName,
                            FolderName),

                    ContentType = achievement.ContentType,
                    MediaType = achievement.MediaType
                })
                .ToList();

            return Result<List<AchievementDto>>.Success(
                response,
                "تم جلب الإنجازات بنجاح.");
        }
    }
}