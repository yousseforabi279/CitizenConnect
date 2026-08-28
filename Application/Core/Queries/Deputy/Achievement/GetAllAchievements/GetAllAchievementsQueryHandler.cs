using Application.Common;
using Application.Contracts;
using Application.Core.Queries.Deputy.Achievement.GetAchievementById;
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
          Result<List<AchievementResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAchievementsQueryHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<AchievementResponse>>> Handle(
            GetAllAchievementsQuery request,
            CancellationToken cancellationToken)
        {

            var achievements =
                    await _unitOfWork.Achievement.GetAllAsync();

            var response = achievements
                    .Select(x => new AchievementResponse
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        Image = x.Image,
                    })
                    .ToList();
            return Result<List<AchievementResponse>>.Success(
                response,
                "تم جلب الإنجازات بنجاح.");
        }
    }
}
