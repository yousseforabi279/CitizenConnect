using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.AreasOfWorkandActivities;
using Application.Core.Queries.Deputy.AreaOfWork.GetById;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.AreaOfWork.GetAll
{
    internal class GetAllAreasOfWorkQueryHandler
      : IRequestHandler<
          GetAllAreasOfWorkQuery,
          Result<List<AreaOfWorkDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "areas-of-work-files";

        public GetAllAreasOfWorkQueryHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<List<AreaOfWorkDTO>>> Handle(
            GetAllAreasOfWorkQuery request,
            CancellationToken cancellationToken)
        {

            var areas = await _unitOfWork.AreasOfWorkandActivities.GetAllAsync();

            var dtos = areas.Select(area => new AreaOfWorkDTO
            {
                Id = area.Id,
                Title = area.Title,
                Description = area.Description,
                MediaUrl = area.BlobName != null
                     ? _blobStorageService.GetReadSasUrl(area.BlobName, ContainerName)
                     : null,
                ContentType = area.ContentType,
                MediaType = area.MediaType
            }).ToList();
            return Result<List<AreaOfWorkDTO>>.Success(
                dtos,
                "تم جلب مجالات العمل بنجاح.");
        }
    }
}
