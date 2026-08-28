using Application.Common;
using Application.Contracts;
using Application.Core.Queries.Deputy.AreaOfWork.GetById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.AreaOfWork.GetAll
{
    internal class GetAllAreasOfWorkQueryHandler
      : IRequestHandler<
          GetAllAreasOfWorkQuery,
          Result<List<AreaOfWorkResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAreasOfWorkQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<AreaOfWorkResponse>>> Handle(
            GetAllAreasOfWorkQuery request,
            CancellationToken cancellationToken)
        {

            var areas = await _unitOfWork.AreasOfWorkandActivities.GetAllAsync();

            var response = areas
                .Select(x => new AreaOfWorkResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Image = x.Image
                })
                .ToList();

            return Result<List<AreaOfWorkResponse>>.Success(
                response,
                "تم جلب مجالات العمل بنجاح.");
        }
    }
}
