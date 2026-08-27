using Application.Common;
using Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.ActivityVisit.EditActivityVisit
{
    internal class UpdateActivityVisitCommandHandler
      : IRequestHandler<UpdateActivityVisitCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateActivityVisitCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            UpdateActivityVisitCommand request,
            CancellationToken cancellationToken)
        {
            var activity = await _unitOfWork.ActitvitiesAndVisits
                .GetByIdAsync(request.ActivityVisitId);

            if (activity is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "النشاط أو الزيارة غير موجود.");
            }

            // Make sure this activity belongs to this deputy
            //if (activity.DeputyId != request.DeputyId)
            //{
            //    return Result<int>.Failure(
            //        ResultStatus.NotFound,
            //        "النشاط أو الزيارة غير موجود لهذا النائب.");
            //}

            activity.Title = request.Title;
            activity.Description = request.Description;
            activity.Image_Video = request.Image_Video;
            activity.Location = request.Location;
            activity.Date = request.Date;

            _unitOfWork.ActitvitiesAndVisits.Update(activity);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                activity.Id,
                "تم التعديل بنجاح.");
        }
    }
}
