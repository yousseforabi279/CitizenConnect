using Application.Common;
using Application.Contracts;
using Domain.Deputy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.CreateAreaOfWork
{
    internal class CreateAreaOfWorkCommandHandler
      : IRequestHandler<CreateAreaOfWorkCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAreaOfWorkCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            CreateAreaOfWorkCommand request,
            CancellationToken cancellationToken)
        {
            var deputy = await _unitOfWork.Deputy
                .GetByIdAsync(request.DeputyId);

            if (deputy is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "النائب غير موجود.");
            }

            var area = new Domain.Deputy.AreasOfWorkandActivities
            {
                DeputyId = request.DeputyId,
                Title = request.Title,
                Description = request.Description,
                Image = request.Image
            };

            await _unitOfWork.AreasOfWorkandActivities
                .AddAsync(area);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                area.Id,
                "تم إضافة مجال العمل بنجاح.");
        }
    }
}
