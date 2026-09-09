using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.AreasOfWorkandActivities;
using Application.storage;
using Domain.Deputy;
using MediatR;

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.CreateAreaOfWork
{
    internal class CreateAreaOfWorkCommandHandler
        : IRequestHandler<CreateAreaOfWorkCommand, Result<AreaOfWorkDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "areas-of-work-files";

        public CreateAreaOfWorkCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<AreaOfWorkDTO>> Handle(
            CreateAreaOfWorkCommand request,
            CancellationToken cancellationToken)
        {
            var area = new Domain.Deputy.AreasOfWorkandActivities
            {
                Title = request.Title,
                Description = request.Description
            };

            if (request.Image != null)
            {
                var upload = await _fileStorageService.UploadFileAsync(
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
                        FolderName);
            }

            await _unitOfWork.AreasOfWorkandActivities.AddAsync(area);

            await _unitOfWork.SaveChangesAsync();

            var dto = new AreaOfWorkDTO
            {
                Id = area.Id,
                Title = area.Title,
                Description = area.Description,

                MediaUrl = !string.IsNullOrEmpty(area.BlobName)
                    ? _fileStorageService.GetFileUrl(
                        area.BlobName,
                        FolderName)
                    : null,

                ContentType = area.ContentType,
                MediaType = area.MediaType
            };

            return Result<AreaOfWorkDTO>.Success(
                dto,
                "تم إضافة مجال العمل بنجاح.");
        }
    }
}