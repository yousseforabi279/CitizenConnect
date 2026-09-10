using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.ActivityVisit;
using Application.storage;
using MediatR;

namespace Application.Core.Queries.Deputy.ActivityVisit.GetAllById
{
    internal class GetActivityVisitQueryHandler
        : IRequestHandler<
            GetActivityVisitQuery,
            Result<ActivityVisitDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "activity-visit-files";

        public GetActivityVisitQueryHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<ActivityVisitDTO>> Handle(
            GetActivityVisitQuery request,
            CancellationToken cancellationToken)
        {
            var activity =
                await _unitOfWork.ActivitiesAndVisits
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

                MediaUrl = string.IsNullOrWhiteSpace(activity.BlobName)
                    ? null
                    : _fileStorageService.GetFileUrl(
                        activity.BlobName,
                        FolderName),

                ContentType = activity.ContentType,
                MediaType = activity.MediaType
            };

            return Result<ActivityVisitDTO>.Success(
                response,
                "تم جلب البيانات بنجاح.");
        }
    }
}