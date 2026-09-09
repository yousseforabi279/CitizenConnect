using Application.Common;
using Application.Contracts;
using Application.storage;
using MediatR;

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.DeleteAreaOfWork
{
    internal class DeleteAreaOfWorkCommandHandler
        : IRequestHandler<DeleteAreaOfWorkCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "areas-of-work-files";

        public DeleteAreaOfWorkCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<int>> Handle(
            DeleteAreaOfWorkCommand request,
            CancellationToken cancellationToken)
        {
            var area =
                await _unitOfWork.AreasOfWorkandActivities
                    .GetByIdAsync(request.AreaId);

            if (area is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "مجال العمل غير موجود.");
            }

            if (!string.IsNullOrEmpty(area.BlobName))
            {
                await _fileStorageService.DeleteFileAsync(
                    area.BlobName,
                    FolderName);
            }

            _unitOfWork.AreasOfWorkandActivities.Delete(area);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                area.Id,
                "تم الحذف بنجاح.");
        }
    }
}