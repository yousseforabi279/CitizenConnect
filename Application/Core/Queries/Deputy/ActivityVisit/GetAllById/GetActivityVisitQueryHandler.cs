using Application.Common;
using Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.ActivityVisit.GetAllById
{
    internal class GetActivityVisitQueryHandler
      : IRequestHandler<
          GetActivityVisitQuery,
          Result<ActivityVisitResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetActivityVisitQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ActivityVisitResponse>> Handle(
            GetActivityVisitQuery request,
            CancellationToken cancellationToken)
        {
            var activity = await _unitOfWork.ActitvitiesAndVisits
                .GetByIdAsync(request.ActivityVisitId);

            if (activity is null)
            {
                return Result<ActivityVisitResponse>.Failure(
                    ResultStatus.NotFound,
                    "النشاط أو الزيارة غير موجود.");
            }

            // Make sure the activity belongs to this deputy
            //if (activity.DeputyId != request.DeputyId)
            //{
            //    return Result<ActivityVisitResponse>.Failure(
            //        ResultStatus.NotFound,
            //        "النشاط أو الزيارة غير موجود لهذا النائب.");
            //}

            var response = new ActivityVisitResponse
            {
                Id = activity.Id,
                Title = activity.Title,
                Description = activity.Description,
                Image_Video = activity.Image_Video,
                Location = activity.Location,
                Date = activity.Date
            };

            return Result<ActivityVisitResponse>.Success(
                response,
                "تم جلب البيانات بنجاح.");
        }
    }
}
