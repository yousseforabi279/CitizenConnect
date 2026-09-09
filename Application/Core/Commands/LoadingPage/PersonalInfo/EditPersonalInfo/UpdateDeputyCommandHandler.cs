using Application.Common;
using Application.Contracts;
using Application.storage;
using Domain.Deputy;
using MediatR;

namespace Application.Core.Commands.LoadingPage.PersonalInfo.EditPersonalInfo
{
    internal class UpdateDeputyCommandHandler
        : IRequestHandler<UpdateDeputyCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "personal-deputy-files";

        public UpdateDeputyCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
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

            // Upload new media if provided
            if (request.Media != null)
            {
                // Delete old media from Cloudinary
                if (!string.IsNullOrEmpty(deputy.BlobName))
                {
                    await _fileStorageService.DeleteFileAsync(
                        deputy.BlobName,
                        FolderName);
                }

                // Upload new media
                var upload =
                    await _fileStorageService.UploadFileAsync(
                        request.Media,
                        FolderName);

                deputy.BlobName = upload.BlobName;
                deputy.MediaFileName = request.Media.FileName;
                deputy.ContentType = upload.ContentType;
                deputy.FileSizeBytes = upload.SizeBytes;

                deputy.MediaType =
                    request.Media.ContentType.StartsWith("video/")
                        ? MediaType.Video
                        : MediaType.Image;

                deputy.UploadedAt = DateTime.UtcNow;

                deputy.MediaUrl =
                    _fileStorageService.GetFileUrl(
                        deputy.BlobName,
                        FolderName);
            }

            // Update deputy information
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