using Application.Common;
using Application.Contracts;
using Application.storage;
using Domain.Deputy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.ActivityVisit.DeleteActivityVisit
{
    internal class DeleteActivityVisitCommandHandler
        : IRequestHandler<
            DeleteActivityVisitCommand,
            Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string ContainerName = "activity-visit-files";
        private readonly IBlobStorageService _blobStorageService;

        public DeleteActivityVisitCommandHandler(
            IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;

        }

        public async Task<Result<int>> Handle(
            DeleteActivityVisitCommand request,
            CancellationToken cancellationToken)
        {
            var activity = await _unitOfWork.ActitvitiesAndVisits
                .GetByIdAsync(request.ActivityVisitId);

            if (activity is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "النشاط أو الزيارة غير موجود.");
            }
            if (!string.IsNullOrEmpty(activity.BlobName))
                await _blobStorageService.DeleteFileAsync(activity.BlobName, ContainerName);

            _unitOfWork.ActitvitiesAndVisits.Delete(activity);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                activity.Id,
                "تم الحذف بنجاح.");
        }
    }
}
