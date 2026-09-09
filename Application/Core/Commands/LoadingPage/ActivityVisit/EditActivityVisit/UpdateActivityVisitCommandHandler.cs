using Application.Common;
using Application.Contracts;
using Application.Core.Commands.Deputy.ActivityVisit.EditActivityVisit;
using Application.Core.Commands.LoadingPage.ActivityVisit;
using Application.storage;
using Domain.Deputy;
using MediatR;

namespace Application.Core.Commands.Deputy.ActivityVisit.EditActivityVisit
{
    internal class UpdateActivityVisitCommandHandler
        : IRequestHandler<
            UpdateActivityVisitCommand,
            Result<ActivityVisitDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "activity-visit-files";

        public UpdateActivityVisitCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<ActivityVisitDTO>> Handle(
            UpdateActivityVisitCommand request,
            CancellationToken cancellationToken)
        {
            var activity =
                await _unitOfWork.ActitvitiesAndVisits
                    .GetByIdAsync(request.Id);

            if (activity is null)
            {
                return Result<ActivityVisitDTO>.Failure(
                    ResultStatus.NotFound,
                    "النشاط غير موجود.");
            }

            // If a new media file was provided
            if (request.Media != null)
            {
                // Delete old media from Cloudinary
                if (!string.IsNullOrEmpty(activity.BlobName))
                {
                    await _fileStorageService.DeleteFileAsync(
                        activity.BlobName,
                        FolderName);
                }

                // Upload new media
                var upload =
                    await _fileStorageService.UploadFileAsync(
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
                        FolderName);
            }

            activity.Title = request.Title;
            activity.Description = request.Description;
            activity.Location = request.Location;
            activity.Date = request.Date;

            await _unitOfWork.SaveChangesAsync();

            var dto = new ActivityVisitDTO
            {
                Id = activity.Id,
                Title = activity.Title,
                Description = activity.Description,
                Location = activity.Location,
                Date = activity.Date,

                MediaUrl = !string.IsNullOrEmpty(activity.BlobName)
                    ? _fileStorageService.GetFileUrl(
                        activity.BlobName,
                        FolderName)
                    : null,

                ContentType = activity.ContentType,
                MediaType = activity.MediaType
            };

            return Result<ActivityVisitDTO>.Success(
                dto,
                "تم تعديل النشاط أو الزيارة بنجاح.");
        }
    }
}