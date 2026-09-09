using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.MotionsForInformation;
using Application.storage;
using MediatR;

namespace Application.Core.Queries.Deputy.MotionsForInformation.GetById
{
    internal class GetMotionsForInformationByIdQueryHandler
        : IRequestHandler<
            GetMotionByIdQuery,
            Result<MotionsForInformationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "motions-for-information-files";

        public GetMotionsForInformationByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<MotionsForInformationDTO>> Handle(
            GetMotionByIdQuery request,
            CancellationToken cancellationToken)
        {
            var motion =
                await _unitOfWork.MotionsForInformation
                    .GetByIdAsync(request.MotionId);

            if (motion is null)
            {
                return Result<MotionsForInformationDTO>.Failure(
                    ResultStatus.NotFound,
                    "الطلب الاستعلامي غير موجود.");
            }

            var dto = new MotionsForInformationDTO
            {
                Id = motion.Id,
                Title = motion.Title,
                Description = motion.Description,

                MediaUrl = string.IsNullOrWhiteSpace(motion.BlobName)
                    ? null
                    : _fileStorageService.GetFileUrl(
                        motion.BlobName,
                        FolderName),

                ContentType = motion.ContentType,
                MediaType = motion.MediaType
            };

            return Result<MotionsForInformationDTO>.Success(dto);
        }
    }
}