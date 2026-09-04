using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.AreasOfWorkandActivities;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.AreaOfWork.GetById
{
    internal class GetAreaOfWorkQueryHandler
       : IRequestHandler<
           GetAreaOfWorkQuery,
           Result<AreaOfWorkDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "areas-of-work-files";

        public GetAreaOfWorkQueryHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }
        public async Task<Result<AreaOfWorkDTO>> Handle(
            GetAreaOfWorkQuery request,
            CancellationToken cancellationToken)
        {
            var area = await _unitOfWork.AreasOfWorkandActivities
                .GetByIdAsync(request.AreaId);

            if (area is null)
            {
                return Result<AreaOfWorkDTO>.Failure(
                    ResultStatus.NotFound,
                    "مجال العمل غير موجود.");
            }

            var dto = new AreaOfWorkDTO
            {
                Id = area.Id,
                Title = area.Title,
                Description = area.Description,
                MediaUrl = area.BlobName != null
                   ? _blobStorageService.GetReadSasUrl(area.BlobName, ContainerName)
                   : null,
                ContentType = area.ContentType,
                MediaType = area.MediaType
            };

            return Result<AreaOfWorkDTO>.Success(
                dto,
                "تم جلب بيانات مجال العمل بنجاح.");
        }
    }
}
