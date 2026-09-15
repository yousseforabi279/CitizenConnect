using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.MotionsForInformation;
using Application.Core.Queries.Deputy.MotionsInforamtion.GetAll.Application.Core.Queries.Deputy.MotionsForInformation.GetAll;
using Application.storage;
using MediatR;

namespace Application.Core.Queries.Deputy.MotionsForInformation.GetAll
{
    internal class GetAllMotionsForInformationQueryHandler
        : IRequestHandler<
            GetAllMotionsQuery,
            Result<List<MotionsForInformationDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "motions-for-information-files";

        public GetAllMotionsForInformationQueryHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<List<MotionsForInformationDTO>>> Handle(
            GetAllMotionsQuery request,
            CancellationToken cancellationToken)
        {
            var motions =
                await _unitOfWork.MotionsForInformation
                    .GetAllAsync();

            var dtos = motions
                .Select(m => new MotionsForInformationDTO
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,

                    MediaUrl = string.IsNullOrWhiteSpace(m.BlobName)
                        ? null
                        : _fileStorageService.GetFileUrl(
                            m.BlobName,
                            FolderName, m.ContentType),

                    ContentType = m.ContentType,
                    MediaType = m.MediaType
                })
                .ToList();

            return Result<List<MotionsForInformationDTO>>.Success(
                dtos);
        }
    }
}