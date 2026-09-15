using Application.Common;
using Application.Contracts;
using Application.storage;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Queries.GetRequestsForDeputy
{
    internal class GetDeputyRequestsQueryHandler
        : IRequestHandler<GetDeputyRequestsQuery, Result<PaginatedResult<DeputyRequestDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        private const string FolderName = "request-files";

        public GetDeputyRequestsQueryHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<PaginatedResult<DeputyRequestDto>>> Handle(
            GetDeputyRequestsQuery request,
            CancellationToken cancellationToken)
        {
            var filter = new DeputyRequestFilter
            {
                Type = request.Type,
                Status = request.Status,
                Priority = request.Priority,
                Name = request.Name,
                Phone = request.Phone,
                NationalId = request.NationalId,
                Title = request.Title,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var result = await _unitOfWork.Deputy
                .GetAllForDeputyAsync(filter, cancellationToken);

            foreach (var item in result.Items)
            {
                if (item.Media is not null && !string.IsNullOrEmpty(item.Media.BlobName))
                {
                    item.Media.MediaUrl = _fileStorageService.GetFileUrl(
                        item.Media.BlobName,
                        FolderName,
                        item.Media.ContentType);
                }
            }

            return Result<PaginatedResult<DeputyRequestDto>>.Success(result);
        }
    }
}