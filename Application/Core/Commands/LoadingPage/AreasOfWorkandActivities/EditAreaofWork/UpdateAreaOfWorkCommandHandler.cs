using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.AreasOfWorkandActivities;
using Application.storage;
using Domain.Deputy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.EditAreaofWork
{
    internal class UpdateAreaOfWorkCommandHandler
    : IRequestHandler<UpdateAreaOfWorkCommand, Result<AreaOfWorkDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "areas-of-work-files";

        public UpdateAreaOfWorkCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<AreaOfWorkDTO>> Handle(
            UpdateAreaOfWorkCommand request,
            CancellationToken cancellationToken)
        {
            var area = await _unitOfWork.AreasOfWorkandActivities.GetByIdAsync(request.AreaId);
            if (area is null)
            {
                return Result<AreaOfWorkDTO>.Failure(ResultStatus.NotFound, "مجال العمل غير موجود.");
            }

            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(area.BlobName))
                    await _blobStorageService.DeleteFileAsync(area.BlobName, ContainerName);

                var upload = await _blobStorageService.UploadFileAsync(request.Image, ContainerName);

                area.BlobName = upload.BlobName;
                area.MediaFileName = request.Image.FileName;
                area.ContentType = upload.ContentType;
                area.FileSizeBytes = upload.SizeBytes;
                area.MediaType = request.Image.ContentType.StartsWith("video") ? MediaType.Video : MediaType.Image;
                area.UploadedAt = DateTime.UtcNow;
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
                MediaUrl = area.BlobName != null
                    ? _blobStorageService.GetReadSasUrl(area.BlobName, ContainerName)
                    : null,
                ContentType = area.ContentType,
                MediaType = area.MediaType
            };

            return Result<AreaOfWorkDTO>.Success(dto, "تم التعديل بنجاح.");
        }
    }
}
