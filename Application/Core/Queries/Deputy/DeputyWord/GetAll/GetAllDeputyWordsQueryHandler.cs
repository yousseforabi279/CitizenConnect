using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.DeputyWords;
using Application.Core.Queries.Deputy.DeputyWord.GetAll;
using Application.storage;
using MediatR;

public class GetAllDeputyWordsQueryHandler
    : IRequestHandler<
        GetAllDeputyWordsQuery,
        Result<List<DeputyWordsDTO>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    private const string FolderName = "deputy-words-files";

    public GetAllDeputyWordsQueryHandler(
        IUnitOfWork unitOfWork,
        IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<List<DeputyWordsDTO>>> Handle(
        GetAllDeputyWordsQuery request,
        CancellationToken cancellationToken)
    {
        var words =
            await _unitOfWork.Deputyword
                .GetAllAsync();

        var dtos = words
            .Select(word => new DeputyWordsDTO
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
            })
            .ToList();

        return Result<List<DeputyWordsDTO>>.Success(dtos);
    }
}