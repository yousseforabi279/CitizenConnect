using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.MotionsForInformation;
using Application.storage;
using Domain.Deputy;
using MediatR;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.CreateMotionsForInformation
{
    internal class CreateMotionsForInformationCommandHandler
        : IRequestHandler<CreateMotionCommand, Result<MotionsForInformationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "motions-for-information-files";

        public CreateMotionsForInformationCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<MotionsForInformationDTO>> Handle(
            CreateMotionCommand request,
            CancellationToken cancellationToken)
        {
            var motion = new Domain.Deputy.MotionsForInformation
            {
                Title = request.Title,
                Description = request.Description
            };

            if (request.Media != null)
            {
                var upload = await _fileStorageService.UploadFileAsync(
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

            await _unitOfWork.MotionsForInformation.AddAsync(motion);

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
                "تمت إضافة الطلب الاستعلامي بنجاح.");
        }
    }
}