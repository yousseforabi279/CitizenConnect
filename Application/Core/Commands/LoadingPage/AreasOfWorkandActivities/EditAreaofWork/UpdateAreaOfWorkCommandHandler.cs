using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.AreasOfWorkandActivities;
using Application.storage;
using Domain.Deputy;
using MediatR;

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.EditAreaofWork
{
    internal class UpdateAreaOfWorkCommandHandler
        : IRequestHandler<UpdateAreaOfWorkCommand, Result<AreaOfWorkDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "areas-of-work-files";

        public UpdateAreaOfWorkCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<AreaOfWorkDTO>> Handle(
            UpdateAreaOfWorkCommand request,
            CancellationToken cancellationToken)
        {
            var area =
                await _unitOfWork.AreasOfWorkandActivities
                    .GetByIdAsync(request.AreaId);

            if (area is null)
            {
                return Result<AreaOfWorkDTO>.Failure(
                    ResultStatus.NotFound,
                    "مجال العمل غير موجود.");
            }

            // Replace old image if a new one was uploaded
            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(area.BlobName))
                {
                    await _fileStorageService.DeleteFileAsync(
                        area.BlobName,
                        FolderName);
                }

                var upload =
                    await _fileStorageService.UploadFileAsync(
                        request.Image,
                        FolderName);

                area.BlobName = upload.BlobName;
                area.MediaFileName = request.Image.FileName;
                area.ContentType = upload.ContentType;
                area.FileSizeBytes = upload.SizeBytes;

                area.MediaType =
                    request.Image.ContentType.StartsWith("video/")
                        ? MediaType.Video
                        : MediaType.Image;

                area.UploadedAt = DateTime.UtcNow;

                area.MediaUrl =
                    _fileStorageService.GetFileUrl(
                        area.BlobName,
                        FolderName,area.ContentType);
            }

            area.Title = request.Title;
            area.Description = request.Description;

            _unitOfWork.AreasOfWorkandActivities.Update(area);

            await _unitOfWork.SaveChangesAsync();

            var dto = new AreaOfWorkDTO
            {
                Id = area.Id,
                Title = area.Title,
                Description = area.Description,

                MediaUrl = !string.IsNullOrEmpty(area.BlobName)
                    ? _fileStorageService.GetFileUrl(
                        area.BlobName,
                        FolderName, area.ContentType)
                    : null,

                ContentType = area.ContentType,
                MediaType = area.MediaType
            };

            return Result<AreaOfWorkDTO>.Success(
                dto,
                "تم التعديل بنجاح.");
        }
    }
}