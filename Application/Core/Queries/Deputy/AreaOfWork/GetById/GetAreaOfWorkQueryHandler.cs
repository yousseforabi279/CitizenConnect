using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.AreasOfWorkandActivities;
using Application.storage;
using MediatR;

namespace Application.Core.Queries.Deputy.AreaOfWork.GetById
{
    internal class GetAreaOfWorkQueryHandler
        : IRequestHandler<
            GetAreaOfWorkQuery,
            Result<AreaOfWorkDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "areas-of-work-files";

        public GetAreaOfWorkQueryHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<AreaOfWorkDTO>> Handle(
            GetAreaOfWorkQuery request,
            CancellationToken cancellationToken)
        {
            var area =
                await _unitOfWork.AreasOfWorkandActivities
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

                MediaUrl = string.IsNullOrWhiteSpace(area.BlobName)
                    ? null
                    : _fileStorageService.GetFileUrl(
                        area.BlobName,
                        FolderName, area.ContentType),

                ContentType = area.ContentType,
                MediaType = area.MediaType
            };

            return Result<AreaOfWorkDTO>.Success(
                dto,
                "تم جلب بيانات مجال العمل بنجاح.");
        }
    }
}