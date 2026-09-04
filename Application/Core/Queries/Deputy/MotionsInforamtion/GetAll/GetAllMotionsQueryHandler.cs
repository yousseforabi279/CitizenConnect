using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.MotionsForInformation;
using Application.Core.Queries.Deputy.MotionsInforamtion.GetAll;
using Application.Core.Queries.Deputy.MotionsInforamtion.GetAll.Application.Core.Queries.Deputy.MotionsForInformation.GetAll;
using Application.storage;
using MediatR;

internal class GetAllMotionsForInformationQueryHandler
    : IRequestHandler<GetAllMotionsQuery, Result<List<MotionsForInformationDTO>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;
    private const string ContainerName = "motions-for-information-files";

    public GetAllMotionsForInformationQueryHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
    {
        _unitOfWork = unitOfWork;
        _blobStorageService = blobStorageService;
    }

    public async Task<Result<List<MotionsForInformationDTO>>> Handle(
        GetAllMotionsQuery request,
        CancellationToken cancellationToken)
    {
        var motions = await _unitOfWork.MotionsForInformation.GetAllAsync();

        var dtos = motions.Select(m => new MotionsForInformationDTO
        {
            Id = m.Id,
            Title = m.Title,
            Description = m.Description,
            MediaUrl = m.BlobName != null
                ? _blobStorageService.GetReadSasUrl(m.BlobName, ContainerName)
                : null,
            ContentType = m.ContentType,
            MediaType = m.MediaType
        }).ToList();

        return Result<List<MotionsForInformationDTO>>.Success(dtos);
    }
}