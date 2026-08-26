using Application.Common;
using Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.ActivityVisit.GetAll
{
    internal class GetAllActivityVisitsQueryHandler
    : IRequestHandler<
        GetAllActivityVisitsQuery,
        Result<List<ActivityVisitResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllActivityVisitsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<ActivityVisitResponse>>> Handle(
            GetAllActivityVisitsQuery request,
            CancellationToken cancellationToken)
        {
            var deputy = await _unitOfWork.Deputy
                .GetByIdAsync(request.DeputyId);

            if (deputy is null)
            {
                return Result<List<ActivityVisitResponse>>.Failure(
                    ResultStatus.NotFound,
                    "النائب غير موجود.");
            }

            var activities =
                         await _unitOfWork.ActitvitiesAndVisits
                             .GetByDeputyIdAsync(
                                 request.DeputyId,
                                 cancellationToken);

            var response = activities
                         .Select(x => new ActivityVisitResponse
                         {
                             Id = x.Id,
                             Title = x.Title,
                             Description = x.Description,
                             Image_Video = x.Image_Video,
                             Location = x.Location,
                             Date = x.Date
                         })
                         .ToList();

            return Result<List<ActivityVisitResponse>>.Success(
                response,
                "تم جلب الأنشطة والزيارات بنجاح.");
        }
    }
}
