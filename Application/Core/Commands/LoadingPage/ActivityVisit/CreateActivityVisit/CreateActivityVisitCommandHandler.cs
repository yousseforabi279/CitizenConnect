using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.ActivityVisit;
using Application.storage;
using Domain.Deputy;
using MediatR;

namespace Application.Core.Commands.Deputy.ActivityVisit.CreateActivityVisit
{
    internal class CreateActivityVisitCommandHandler
        : IRequestHandler<CreateActivityVisitCommand, Result<ActivityVisitDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;

        private const string ContainerName = "activity-visit-files";

        public CreateActivityVisitCommandHandler(
            IUnitOfWork unitOfWork,
            IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<ActivityVisitDTO>> Handle(
            CreateActivityVisitCommand request,
            CancellationToken cancellationToken)
        {
            var activity = new ActitvitiesAndVisits
            {
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                Date = request.Date
            };

            if (request.Media != null)
            {
                var upload = await _blobStorageService.UploadFileAsync(request.Media, ContainerName);

                activity.BlobName = upload.BlobName;
                activity.MediaFileName = request.Media.FileName;
                activity.ContentType = upload.ContentType;
                activity.FileSizeBytes = upload.SizeBytes;
                activity.MediaType = request.Media.ContentType.StartsWith("video")
                    ? MediaType.Video
                    : MediaType.Image;
                activity.UploadedAt = DateTime.UtcNow;
            }

            await _unitOfWork.ActitvitiesAndVisits.AddAsync(activity);
            await _unitOfWork.SaveChangesAsync();

            var dto = MapToDto(activity);

            return Result<ActivityVisitDTO>.Success(
                dto,
                "تمت إضافة النشاط أو الزيارة بنجاح.");
        }

        private ActivityVisitDTO MapToDto(ActitvitiesAndVisits activity) => new()
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
    }
}