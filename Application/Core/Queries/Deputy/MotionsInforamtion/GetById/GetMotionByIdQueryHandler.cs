using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.MotionsForInformation;
using Application.Core.Queries.Deputy.MotionsInforamtion.GetAll;
using Application.storage;
using MediatR;

namespace Application.Core.Queries.Deputy.MotionsForInformation.GetById
{
    internal class GetMotionsForInformationByIdQueryHandler
    : IRequestHandler<GetMotionByIdQuery, Result<MotionsForInformationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "motions-for-information-files";

        public GetMotionsForInformationByIdQueryHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<MotionsForInformationDTO>> Handle(
            GetMotionByIdQuery request,
            CancellationToken cancellationToken)
        {
            var motion = await _unitOfWork.MotionsForInformation.GetByIdAsync(request.MotionId);
            if (motion is null)
            {
                return Result<MotionsForInformationDTO>.Failure(ResultStatus.NotFound, "الطلب الاستعلامي غير موجود.");
            }

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

            return Result<MotionsForInformationDTO>.Success(dto);
        }
    }
}