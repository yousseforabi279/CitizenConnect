using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.achievements;
using Application.Core.Queries.Deputy.Achievement.GetAchievementById;
using Application.storage;
using Domain.Deputy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.Achievement.GetAllAchievements
{
    internal class GetAllAchievementsQueryHandler
      : IRequestHandler<
          GetAllAchievementsQuery,
          Result<List<AchievementDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "achievement-files";


        public GetAllAchievementsQueryHandler(
            IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;

        }

        public async Task<Result<List<AchievementDto>>> Handle(
            GetAllAchievementsQuery request,
            CancellationToken cancellationToken)
        {

            var achievements =
                    await _unitOfWork.Achievement.GetAllAsync();

            var response = achievements
                .Select(achievement => new AchievementDto
                {
                    Id = achievement.Id,
                    Title = achievement.Title,
                    Description = achievement.Description,
                    MediaUrl = string.IsNullOrWhiteSpace(achievement.BlobName)
                        ? null
                        : _blobStorageService.GetReadSasUrl(achievement.BlobName,ContainerName),
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
