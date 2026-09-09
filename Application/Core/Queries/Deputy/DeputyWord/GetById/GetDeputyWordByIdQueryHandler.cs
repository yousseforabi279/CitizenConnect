using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.DeputyWords;
using Application.Core.Queries.Deputy.DeputyWord.GetById;
using Application.storage;
using MediatR;

public class GetDeputyWordByIdQueryHandler
    : IRequestHandler<
        GetDeputyWordByIdQuery,
        Result<DeputyWordsDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    private const string FolderName = "deputy-words-files";

    public GetDeputyWordByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<DeputyWordsDTO>> Handle(
        GetDeputyWordByIdQuery request,
        CancellationToken cancellationToken)
    {
        var word =
            await _unitOfWork.Deputyword
                .GetByIdAsync(request.Id);

        if (word is null)
        {
            return Result<DeputyWordsDTO>.Failure(
                ResultStatus.NotFound,
                "كلمة النائب غير موجودة.");
        }

        var dto = new DeputyWordsDTO
        {
            Id = word.Id,
            Title = word.Title,

            MediaUrl = string.IsNullOrWhiteSpace(word.BlobName)
                ? null
                : _fileStorageService.GetFileUrl(
                    word.BlobName,
                    FolderName),

            ContentType = word.ContentType,
            MediaType = word.MediaType
        };

        return Result<DeputyWordsDTO>.Success(dto);
    }
}