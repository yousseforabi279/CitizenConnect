using Application.Common;
using Application.Contracts;
using Application.storage;
using Domain.Deputy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.PersonalInfo.EditPersonalInfo
{
    internal class UpdateDeputyCommandHandler
       : IRequestHandler<UpdateDeputyCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _BlobStorageService;
        private const string ContainerName = "PersoalDeputy-files";

        public UpdateDeputyCommandHandler(IUnitOfWork unitOfWork,IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _BlobStorageService = blobStorageService;
        }
        public async Task<Result<int>> Handle(
               UpdateDeputyCommand request,
               CancellationToken cancellationToken)
        {
            var deputy = await _unitOfWork.Deputy
                .GetDeputyInfo();

            if (deputy is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "النائب غير موجود.");
            }
            if (request.Media != null)
            {
                if (!string.IsNullOrEmpty(deputy.BlobName))
                    await _BlobStorageService.DeleteFileAsync(deputy.BlobName, ContainerName);

                var upload = await _BlobStorageService.UploadFileAsync(request.Media, ContainerName);

                deputy.BlobName = upload.BlobName;
                deputy.MediaFileName = request.Media.FileName;
                deputy.ContentType = upload.ContentType;
                deputy.FileSizeBytes = upload.SizeBytes;
                deputy.MediaType = request.Media.ContentType.StartsWith("video") ? MediaType.Video : MediaType.Image;
                deputy.UploadedAt = DateTime.UtcNow;
                deputy.MediaUrl = deputy.BlobName != null ? _BlobStorageService.GetReadSasUrl(deputy.BlobName, ContainerName) : null;

            }

            deputy.FullName = request.FullName;
            deputy.BirthOfdate = request.BirthOfdate;
            deputy.PrimaryPhone = request.PrimaryPhone;
            deputy.SecondaryPhone = request.SecondaryPhone;
            deputy.Address = request.Address;
            deputy.Title = request.Title;
            deputy.Bio = request.Bio;
            deputy.AboutPart1 = request.AboutPart1;
            deputy.AboutPart2 = request.AboutPart2;
            deputy.OfficeLocation = request.OfficeLocation;
            deputy.WhatsApp = request.WhatsApp;
            deputy.FacebookLing = request.FacebookLing;
            deputy.LocationURL = request.LocationURL;
            deputy.Circle = request.Circle;
            deputy.Appointment = request.Appointment;

            _unitOfWork.Deputy.Update(deputy);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                deputy.Id,
                "تم التعديل بنجاح");
        }
    }
}