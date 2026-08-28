using Application.Common;
using Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.AreaOfWork.GetById
{
    internal class GetAreaOfWorkQueryHandler
       : IRequestHandler<
           GetAreaOfWorkQuery,
           Result<AreaOfWorkResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAreaOfWorkQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AreaOfWorkResponse>> Handle(
            GetAreaOfWorkQuery request,
            CancellationToken cancellationToken)
        {
            var area = await _unitOfWork.AreasOfWorkandActivities
                .GetByIdAsync(request.AreaId);

            if (area is null)
            {
                return Result<AreaOfWorkResponse>.Failure(
                    ResultStatus.NotFound,
                    "مجال العمل غير موجود.");
            }

            var response = new AreaOfWorkResponse
            {
                Id = area.Id,
                Title = area.Title,
                Description = area.Description,
                Image = area.Image
            };

            return Result<AreaOfWorkResponse>.Success(
                response,
                "تم جلب بيانات مجال العمل بنجاح.");
        }
    }
}
