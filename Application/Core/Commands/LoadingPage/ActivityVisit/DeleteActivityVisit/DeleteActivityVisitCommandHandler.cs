using Application.Common;
using Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.ActivityVisit.DeleteActivityVisit
{
    internal class DeleteActivityVisitCommandHandler
        : IRequestHandler<
            DeleteActivityVisitCommand,
            Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteActivityVisitCommandHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            DeleteActivityVisitCommand request,
            CancellationToken cancellationToken)
        {
            var activity = await _unitOfWork.ActitvitiesAndVisits
                .GetByIdAsync(request.ActivityVisitId);

            if (activity is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "النشاط أو الزيارة غير موجود.");
            }

            _unitOfWork.ActitvitiesAndVisits.Delete(activity);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                activity.Id,
                "تم الحذف بنجاح.");
        }
    }
}
