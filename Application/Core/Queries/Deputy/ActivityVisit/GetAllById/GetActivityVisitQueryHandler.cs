using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.ActivityVisit;
using Application.storage;
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
          Result<ActivityVisitDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "activity-visit-files";

        public GetActivityVisitQueryHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<ActivityVisitDTO>> Handle(
            GetActivityVisitQuery request,
            CancellationToken cancellationToken)
        {
            var activity = await _unitOfWork.ActitvitiesAndVisits
                .GetByIdAsync(request.ActivityVisitId);

            if (activity is null)
            {
                return Result<ActivityVisitDTO>.Failure(
                    ResultStatus.NotFound,
                    "النشاط أو الزيارة غير موجود.");
            }


            var response = new ActivityVisitDTO
            {
                Id = activity.Id,
                Title = activity.Title,
                Description = activity.Description,
                Location = activity.Location,
                Date = activity.Date,
                MediaUrl = activity.BlobName != null
                    ? _blobStorageService.GetReadSasUrl(activity.BlobName, ContainerName)
                    : null,
                ContentType = activity.ContentType,
                MediaType = activity.MediaType
            };

            return Result<ActivityVisitDTO>.Success(
                response,
                "تم جلب البيانات بنجاح.");
        }
    }
}
