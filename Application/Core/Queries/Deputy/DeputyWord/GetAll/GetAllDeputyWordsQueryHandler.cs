using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Commands.LoadingPage.DeputyWords;
using Application.Core.Queries.Deputy.AreaOfWork.GetById;
using Application.Core.Queries.Deputy.DeputyWord.GetAll;
using Application.storage;
using Domain.Deputy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

public class GetAllDeputyWordsQueryHandler
    : IRequestHandler<GetAllDeputyWordsQuery, Result<List<DeputyWordsDTO>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;
    private const string ContainerName = "deputy-words-files";

    public GetAllDeputyWordsQueryHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
    {
        _unitOfWork = unitOfWork;
        _blobStorageService = blobStorageService;
    }


    public async Task<Result<List<DeputyWordsDTO>>> Handle(
        GetAllDeputyWordsQuery request,
        CancellationToken cancellationToken)
    {
        var words = await _unitOfWork.Deputyword.GetAllAsync();

        var dtos = words.Select(word => new DeputyWordsDTO
        {
            Id = word.Id,
            Title = word.Title,
            MediaUrl = string.IsNullOrWhiteSpace(word.BlobName)
                        ? null
                        : _blobStorageService.GetReadSasUrl(word.BlobName, ContainerName),
            ContentType = word.ContentType,
            MediaType = word.MediaType
        }).ToList();

        return Result<List<DeputyWordsDTO>>.Success(dtos);
    }
}