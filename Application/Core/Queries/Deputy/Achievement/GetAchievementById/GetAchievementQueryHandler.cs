using Application.Common;
using Application.Contracts;
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
         Result<AchievementResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAchievementQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AchievementResponse>> Handle(
            GetAchievementQuery request,
            CancellationToken cancellationToken)
        {
            var achievement = await _unitOfWork.Achievement
                .GetByIdAsync(request.AchievementId);

            if (achievement is null)
            {
                return Result<AchievementResponse>.Failure(
                    ResultStatus.NotFound,
                    "الإنجاز غير موجود.");
            }

            //if (achievement.DeputyId != request.DeputyId)
            //{
            //    return Result<AchievementResponse>.Failure(
            //        ResultStatus.NotFound,
            //        "الإنجاز غير موجود لهذا النائب.");
            //}

            var response = new AchievementResponse
            {
                Id = achievement.Id,
                Title = achievement.Title,
                Description = achievement.Description,
                Image = achievement.Image,
            };

            return Result<AchievementResponse>.Success(
                response,
                "تم جلب بيانات الإنجاز بنجاح.");
        }
    }
}
