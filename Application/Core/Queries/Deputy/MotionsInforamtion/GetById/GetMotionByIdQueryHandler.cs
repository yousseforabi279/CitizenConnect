using Application.Common;
using Application.Contracts;
using Application.Core.Queries.Deputy.MotionsInforamtion.GetAll;
using MediatR;

namespace Application.Core.Queries.Deputy.MotionsForInformation.GetById
{
    public class GetMotionByIdQueryHandler
        : IRequestHandler<GetMotionByIdQuery, Result<MotionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMotionByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MotionDto>> Handle(
            GetMotionByIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.MotionsForInformation
                .GetByIdAsync(request.MotionId);

            if (result == null)
            {
                return Result<MotionDto>.Failure(
                    ResultStatus.Failure,
                    "الحركة غير موجودة.");
            }

            var response = new MotionDto
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                Image_Video = result.Image_Video
            };

            return Result<MotionDto>.Success(
                response,
                "تم جلب الحركة بنجاح.");
        }
    }
}