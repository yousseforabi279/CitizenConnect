using Application.Common;
using Application.Contracts;
using Application.Core.Queries.Deputy.AreaOfWork.GetById;
using Application.Core.Queries.Deputy.DeputyWord.GetAll;
using Domain.Deputy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

public class GetAllDeputyWordsQueryHandler
    : IRequestHandler<GetAllDeputyWordsQuery, Result<List<DeputyWordsDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDeputyWordsQueryHandler(IUnitOfWork context)
    {
        _unitOfWork = context;
    }

    public async Task<Result<List<DeputyWordsDto>>> Handle(
        GetAllDeputyWordsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Deputyword.GetAllAsync();
        var response = result
                .Select(x => new DeputyWordsDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Video_image = x.Video_image
                })
                .ToList();
        return Result<List<DeputyWordsDto>>.Success(
             response,
             "تم جلب مجالات العمل بنجاح.");
    }
}