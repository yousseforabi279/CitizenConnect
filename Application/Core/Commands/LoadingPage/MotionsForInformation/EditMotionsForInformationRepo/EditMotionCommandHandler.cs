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
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "motions-for-information-files";

        public UpdateMotionsForInformationCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<MotionsForInformationDTO>> Handle(
            EditMotionCommand request,
            CancellationToken cancellationToken)
        {
            var motion = await _unitOfWork.MotionsForInformation.GetByIdAsync(request.Id);
            if (motion is null)
            {
                return Result<MotionsForInformationDTO>.Failure(ResultStatus.NotFound, "الطلب الاستعلامي غير موجود.");
            }

            if (request.Media != null)
            {
                if (!string.IsNullOrEmpty(motion.BlobName))
                    await _blobStorageService.DeleteFileAsync(motion.BlobName, ContainerName);

                var upload = await _blobStorageService.UploadFileAsync(request.Media, ContainerName);

                motion.BlobName = upload.BlobName;
                motion.MediaFileName = request.Media.FileName;
                motion.ContentType = upload.ContentType;
                motion.FileSizeBytes = upload.SizeBytes;
                motion.MediaType = request.Media.ContentType.StartsWith("video") ? MediaType.Video : MediaType.Image;
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
                MediaUrl = motion.BlobName != null
                    ? _blobStorageService.GetReadSasUrl(motion.BlobName, ContainerName)
                    : null,
                ContentType = motion.ContentType,
                MediaType = motion.MediaType
            };

            return Result<MotionsForInformationDTO>.Success(dto, "تم التعديل بنجاح.");
        }
    }
}