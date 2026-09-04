using Application.Common;
using Application.Contracts;
using Application.storage;
using MediatR;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.DeleteMotionsForInformation
{
    internal class DeleteMotionsForInformationCommandHandler
     : IRequestHandler<DeleteMotionCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "motions-for-information-files";

        public DeleteMotionsForInformationCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<int>> Handle(
            DeleteMotionCommand request,
            CancellationToken cancellationToken)
        {
            var motion = await _unitOfWork.MotionsForInformation.GetByIdAsync(request.MotionId);
            if (motion is null)
            {
                return Result<int>.Failure(ResultStatus.NotFound, "الطلب الاستعلامي غير موجود.");
            }

            if (!string.IsNullOrEmpty(motion.BlobName))
                await _blobStorageService.DeleteFileAsync(motion.BlobName, ContainerName);

            _unitOfWork.MotionsForInformation.Delete(motion);
            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(motion.Id, "تم الحذف بنجاح.");
        }
    }
}