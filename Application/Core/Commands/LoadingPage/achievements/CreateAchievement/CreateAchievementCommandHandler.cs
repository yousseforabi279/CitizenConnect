using Application.Common;
using Application.Contracts;
using Domain.Deputy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.achievements.CreateAchievement
{
    internal class CreateAchievementCommandHandler
    : IRequestHandler<CreateAchievementCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAchievementCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            CreateAchievementCommand request,
            CancellationToken cancellationToken)
        {
            var achievement = new Achievement
            {
                Title = request.Title,
                Description = request.Description,
                Image = request.Image,
            };
            await _unitOfWork.Achievement.AddAsync(achievement);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                achievement.Id,
                "تم إضافة الإنجاز بنجاح.");
        }
    }

}
