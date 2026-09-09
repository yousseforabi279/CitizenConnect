using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.AreasOfWorkandActivities;
using Application.storage;
using MediatR;

namespace Application.Core.Queries.Deputy.AreaOfWork.GetAll
{
    internal class GetAllAreasOfWorkQueryHandler
        : IRequestHandler<
            GetAllAreasOfWorkQuery,
            Result<List<AreaOfWorkDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "areas-of-work-files";

        public GetAllAreasOfWorkQueryHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<List<AreaOfWorkDTO>>> Handle(
            GetAllAreasOfWorkQuery request,
            CancellationToken cancellationToken)
        {
            var areas =
                await _unitOfWork.AreasOfWorkandActivities
                    .GetAllAsync();

            var dtos = areas
                .Select(area => new AreaOfWorkDTO
                {
                    Id = area.Id,
                    Title = area.Title,
                    Description = area.Description,

                    MediaUrl = string.IsNullOrWhiteSpace(area.BlobName)
                        ? null
                        : _fileStorageService.GetFileUrl(
                            area.BlobName,
                            FolderName),

                    ContentType = area.ContentType,
                    MediaType = area.MediaType
                })
                .ToList();

            return Result<List<AreaOfWorkDTO>>.Success(
                dtos,
                "تم جلب مجالات العمل بنجاح.");
        }
    }
}