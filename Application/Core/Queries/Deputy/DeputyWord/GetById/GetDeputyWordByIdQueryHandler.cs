
using Application.Common;
using Application.Contracts;
using Application.Core.Queries.Deputy.DeputyWord.GetAll;
using Application.Core.Queries.Deputy.DeputyWord.GetById;
using Domain.Deputy;
using MediatR;

public class GetDeputyWordByIdQueryHandler
    : IRequestHandler<GetDeputyWordByIdQuery, Result<DeputyWordsDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDeputyWordByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DeputyWordsDto>> Handle(
        GetDeputyWordByIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Deputyword
            .GetByIdAsync(request.Id);

        if (result == null)
        {
            return Result<DeputyWordsDto>.Failure(
                ResultStatus.NotFound,
                "كلمة النائب غير موجودة.");
        }

        var response = new DeputyWordsDto
        {
            Id = result.Id,
            Title = result.Title,
            Video_image = result.Video_image
        };

        return Result<DeputyWordsDto>.Success(
            response,
            "تم جلب كلمة النائب بنجاح.");
    }
}
