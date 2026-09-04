using Application.Common;
using Application.Contracts;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.DeleteAreaOfWork
{
    internal class DeleteAreaOfWorkCommandHandler
    : IRequestHandler<DeleteAreaOfWorkCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "areas-of-work-files";

        public DeleteAreaOfWorkCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<int>> Handle(
            DeleteAreaOfWorkCommand request,
            CancellationToken cancellationToken)
        {
            var area = await _unitOfWork.AreasOfWorkandActivities.GetByIdAsync(request.AreaId);
            if (area is null)
            {
                return Result<int>.Failure(ResultStatus.NotFound, "مجال العمل غير موجود.");
            }

            if (!string.IsNullOrEmpty(area.BlobName))
                await _blobStorageService.DeleteFileAsync(area.BlobName, ContainerName);

            _unitOfWork.AreasOfWorkandActivities.Delete(area);
            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(area.Id, "تم الحذف بنجاح.");
        }
    }
}
