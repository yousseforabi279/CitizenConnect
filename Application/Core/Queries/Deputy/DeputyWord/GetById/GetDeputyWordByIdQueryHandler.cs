
using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.DeputyWords;
using Application.Core.Queries.Deputy.DeputyWord.GetAll;
using Application.Core.Queries.Deputy.DeputyWord.GetById;
using Application.storage;
using Domain.Deputy;
using MediatR;

public class GetDeputyWordByIdQueryHandler
    : IRequestHandler<GetDeputyWordByIdQuery, Result<DeputyWordsDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;
    private const string ContainerName = "deputy-words-files";

    public GetDeputyWordByIdQueryHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
    {
        _unitOfWork = unitOfWork;
        _blobStorageService = blobStorageService;
    }

    public async Task<Result<DeputyWordsDTO>> Handle(
        GetDeputyWordByIdQuery request,
        CancellationToken cancellationToken)
    {
        var word = await _unitOfWork.Deputyword.GetByIdAsync(request.Id);
        if (word is null)
        {
            return Result<DeputyWordsDTO>.Failure(ResultStatus.NotFound, "كلمة النائب غير موجودة.");
        }

        var dto = new DeputyWordsDTO
        {
            Id = word.Id,
            Title = word.Title,
            MediaUrl = _blobStorageService.GetReadSasUrl(word.BlobName, ContainerName),
            ContentType = word.ContentType,
            MediaType = word.MediaType
        };

        return Result<DeputyWordsDTO>.Success(dto);
    }
}
