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

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.CreateAreaOfWork
{
        internal class CreateAreaOfWorkCommandHandler
            : IRequestHandler<CreateAreaOfWorkCommand, Result<AreaOfWorkDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IBlobStorageService _blobStorageService;
            private const string ContainerName = "areas-of-work-files";

            public CreateAreaOfWorkCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
            {
                _unitOfWork = unitOfWork;
                _blobStorageService = blobStorageService;
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

                var upload = await _blobStorageService.UploadFileAsync(request.Image, ContainerName);

                area.BlobName = upload.BlobName;
                area.MediaFileName = request.Image.FileName;
                area.ContentType = upload.ContentType;
                area.FileSizeBytes = upload.SizeBytes;
                area.MediaType = request.Image.ContentType.StartsWith("video") ? MediaType.Video : MediaType.Image;
                area.UploadedAt = DateTime.UtcNow;

                await _unitOfWork.AreasOfWorkandActivities.AddAsync(area);
                await _unitOfWork.SaveChangesAsync();

                var dto = new AreaOfWorkDTO
                {
                    Id = area.Id,
                    Title = area.Title,
                    Description = area.Description,
                    MediaUrl = _blobStorageService.GetReadSasUrl(area.BlobName, ContainerName),
                    ContentType = area.ContentType,
                    MediaType = area.MediaType
                };

                return Result<AreaOfWorkDTO>.Success(dto, "تم إضافة مجال العمل بنجاح.");
            }
        }
    }
