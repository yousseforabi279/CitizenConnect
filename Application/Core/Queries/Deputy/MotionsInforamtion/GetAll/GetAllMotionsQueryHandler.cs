using Application.Common;
using Application.Contracts;
using Application.Core.Queries.Deputy.MotionsInforamtion.GetAll;
using Application.Core.Queries.Deputy.MotionsInforamtion.GetAll.Application.Core.Queries.Deputy.MotionsForInformation.GetAll;
using MediatR;

public class GetAllMotionsQueryHandler
    : IRequestHandler<GetAllMotionsQuery, Result<List<MotionDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllMotionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<MotionDto>>> Handle(
        GetAllMotionsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MotionsForInformation
            .GetAllAsync();

        var response = result
            .Select(x => new MotionDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Image_Video = x.Image_Video
            })
            .ToList();

        return Result<List<MotionDto>>.Success(
            response,
            "تم جلب الحركات بنجاح.");
    }
}