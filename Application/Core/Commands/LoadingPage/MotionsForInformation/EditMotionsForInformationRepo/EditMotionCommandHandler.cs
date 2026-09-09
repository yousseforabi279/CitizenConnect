using Application.Common;
using Application.Contracts;
using Application.storage;
using Domain.Deputy;
using MediatR;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.EditMotionsForInformation
{
    internal class UpdateMotionsForInformationCommandHandler
        : IRequestHandler<EditMotionCommand, Result<MotionsForInformationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "motions-for-information-files";

        public UpdateMotionsForInformationCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<MotionsForInformationDTO>> Handle(
            EditMotionCommand request,
            CancellationToken cancellationToken)
        {
            var motion =
                await _unitOfWork.MotionsForInformation
                    .GetByIdAsync(request.Id);

            if (motion is null)
            {
                return Result<MotionsForInformationDTO>.Failure(
                    ResultStatus.NotFound,
                    "الطلب الاستعلامي غير موجود.");
            }

            if (request.Media != null)
            {
                // Delete old file from Cloudinary
                if (!string.IsNullOrEmpty(motion.BlobName))
                {
                    await _fileStorageService.DeleteFileAsync(
                        motion.BlobName,
                        FolderName);
                }

                // Upload new file
                var upload =
                    await _fileStorageService.UploadFileAsync(
                        request.Media,
                        FolderName);

                motion.BlobName = upload.BlobName;
                motion.MediaFileName = request.Media.FileName;
                motion.ContentType = upload.ContentType;
                motion.FileSizeBytes = upload.SizeBytes;

                motion.MediaType =
                    request.Media.ContentType.StartsWith("video/")
                        ? MediaType.Video
                        : MediaType.Image;

                motion.UploadedAt = DateTime.UtcNow;

            }

            motion.Title = request.Title;
            motion.Description = request.Description;

            _unitOfWork.MotionsForInformation.Update(motion);

            await _unitOfWork.SaveChangesAsync();

            var dto = new MotionsForInformationDTO
            {
                Id = motion.Id,
                Title = motion.Title,
                Description = motion.Description,

                MediaUrl = !string.IsNullOrEmpty(motion.BlobName)
                    ? _fileStorageService.GetFileUrl(
                        motion.BlobName,
                        FolderName)
                    : null,

                ContentType = motion.ContentType,
                MediaType = motion.MediaType
            };

            return Result<MotionsForInformationDTO>.Success(
                dto,
                "تم التعديل بنجاح.");
        }
    }
}