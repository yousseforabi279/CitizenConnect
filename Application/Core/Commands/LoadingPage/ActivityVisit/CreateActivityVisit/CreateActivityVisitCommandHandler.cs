using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
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
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "activity-visit-files";

        public CreateActivityVisitCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
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
                var upload = await _fileStorageService.UploadFileAsync(
                    request.Media,
                    FolderName);

                activity.BlobName = upload.BlobName;
                activity.MediaFileName = request.Media.FileName;
                activity.ContentType = upload.ContentType;
                activity.FileSizeBytes = upload.SizeBytes;

                activity.MediaType =
                    request.Media.ContentType.StartsWith("video/")
                        ? MediaType.Video
                        : MediaType.Image;

                activity.UploadedAt = DateTime.UtcNow;
                activity.MediaUrl =
                   _fileStorageService.GetFileUrl(
                       activity.BlobName,
                       FolderName, activity.ContentType);


            }

            await _unitOfWork.ActitvitiesAndVisits.AddAsync(activity);

            await _unitOfWork.SaveChangesAsync();

            var dto = MapToDto(activity);

            return Result<ActivityVisitDTO>.Success(
                dto,
                "تمت إضافة النشاط أو الزيارة بنجاح.");
        }

        private ActivityVisitDTO MapToDto(
            ActitvitiesAndVisits activity)
        {
            return new ActivityVisitDTO
            {
                Id = activity.Id,
                Title = activity.Title,
                Description = activity.Description,
                Location = activity.Location,
                Date = activity.Date,

                MediaUrl = !string.IsNullOrEmpty(activity.BlobName)
                    ? _fileStorageService.GetFileUrl(
                        activity.BlobName,
                        FolderName, activity.ContentType)
                    : null,

                ContentType = activity.ContentType,
                MediaType = activity.MediaType
            };
        }
    }
}