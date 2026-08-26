using Application.Common;
using Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.EditAreaofWork
{
    internal class UpdateAreaOfWorkCommandHandler
      : IRequestHandler<UpdateAreaOfWorkCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAreaOfWorkCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            UpdateAreaOfWorkCommand request,
            CancellationToken cancellationToken)
        {
            var area = await _unitOfWork.AreasOfWorkandActivities
                .GetByIdAsync(request.AreaId);

            if (area is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "مجال العمل غير موجود.");
            }

            // Make sure this area belongs to this Deputy
            if (area.DeputyId != request.DeputyId)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "مجال العمل غير موجود لهذا النائب.");
            }

            area.Title = request.Title;
            area.Description = request.Description;
            area.Image = request.Image;

            _unitOfWork.AreasOfWorkandActivities.Update(area);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                area.Id,
                "تم التعديل بنجاح.");
        }
    }
}
