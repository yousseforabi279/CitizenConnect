using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.ActivityVisit;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.ActivityVisit.GetAll
{
    internal class GetAllActivityVisitsQueryHandler
    : IRequestHandler<
        GetAllActivityVisitsQuery,
        Result<List<ActivityVisitDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "activity-visit-files";

        public GetAllActivityVisitsQueryHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;

        }

        public async Task<Result<List<ActivityVisitDTO>>> Handle(
            GetAllActivityVisitsQuery request,
            CancellationToken cancellationToken)
        {

            var activities =
                         await _unitOfWork.ActitvitiesAndVisits.GetAllAsync();

            var response = activities
                         .Select(a => new ActivityVisitDTO
                         {
                             Id = a.Id,
                             Title = a.Title,
                             Description = a.Description,
                             Location = a.Location,
                             Date = a.Date,
                             MediaUrl = a.BlobName != null
                                ? _blobStorageService.GetReadSasUrl(a.BlobName, ContainerName)
                                : null,
                             ContentType = a.ContentType,
                             MediaType = a.MediaType
                         }).ToList();

            return Result<List<ActivityVisitDTO>>.Success(
                response,
                "تم جلب الأنشطة والزيارات بنجاح.");
        }
    }
}
