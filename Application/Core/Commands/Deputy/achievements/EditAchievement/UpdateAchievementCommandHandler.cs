using Application.Common;
using Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.achievements.EditAchievement
{
    internal class UpdateAchievementCommandHandler
     : IRequestHandler<UpdateAchievementCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAchievementCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            UpdateAchievementCommand request,
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

            // Make sure this achievement belongs to this deputy
            if (achievement.DeputyId != request.DeputyId)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "الإنجاز غير موجود لهذا النائب.");
            }

            achievement.Title = request.Title;
            achievement.Description = request.Description;
            achievement.Image = request.Image;

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                achievement.Id,
                "تم تعديل الإنجاز بنجاح.");
        }
    }
}
