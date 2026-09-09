using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.ActivityVisit;
using Application.storage;
using MediatR;

namespace Application.Core.Queries.Deputy.ActivityVisit.GetAll
{
    internal class GetAllActivityVisitsQueryHandler
        : IRequestHandler<
            GetAllActivityVisitsQuery,
            Result<List<ActivityVisitDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "activity-visit-files";

        public GetAllActivityVisitsQueryHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<List<ActivityVisitDTO>>> Handle(
            GetAllActivityVisitsQuery request,
            CancellationToken cancellationToken)
        {
            var activities =
                await _unitOfWork.ActitvitiesAndVisits
                    .GetAllAsync();

            var response = activities
                .Select(a => new ActivityVisitDTO
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Location = a.Location,
                    Date = a.Date,

                    MediaUrl = string.IsNullOrWhiteSpace(a.BlobName)
                        ? null
                        : _fileStorageService.GetFileUrl(
                            a.BlobName,
                            FolderName),

                    ContentType = a.ContentType,
                    MediaType = a.MediaType
                })
                .ToList();

            return Result<List<ActivityVisitDTO>>.Success(
                response,
                "تم جلب الأنشطة والزيارات بنجاح.");
        }
    }
}