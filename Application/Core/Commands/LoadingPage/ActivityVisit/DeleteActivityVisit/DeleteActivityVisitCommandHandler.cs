using Application.Common;
using Application.Contracts;
using Application.storage;
using MediatR;

namespace Application.Core.Commands.Deputy.ActivityVisit.DeleteActivityVisit
{
    internal class DeleteActivityVisitCommandHandler
        : IRequestHandler<
            DeleteActivityVisitCommand,
            Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "activity-visit-files";

        public DeleteActivityVisitCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<int>> Handle(
            DeleteActivityVisitCommand request,
            CancellationToken cancellationToken)
        {
            var activity = await _unitOfWork.ActivitiesAndVisits
                .GetByIdAsync(request.ActivityVisitId);

            if (activity is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "النشاط أو الزيارة غير موجود.");
            }

            // Delete media from Cloudinary
            if (!string.IsNullOrEmpty(activity.BlobName))
            {
                await _fileStorageService.DeleteFileAsync(
                    activity.BlobName,
                    FolderName);
            }

            // Delete from database
            _unitOfWork.ActivitiesAndVisits.Delete(activity);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                activity.Id,
                "تم الحذف بنجاح.");
        }
    }
}