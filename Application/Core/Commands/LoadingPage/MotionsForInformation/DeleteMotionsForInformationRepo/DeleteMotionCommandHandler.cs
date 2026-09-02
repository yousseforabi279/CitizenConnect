using Application.Common;
using Application.Contracts;
using MediatR;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.DeleteMotionsForInformation
{
    public class DeleteMotionCommandHandler
        : IRequestHandler<DeleteMotionCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMotionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            DeleteMotionCommand request,
            CancellationToken cancellationToken)
        {
            var motion = await _unitOfWork.MotionsForInformation
                .GetByIdAsync(request.MotionId);

            if (motion == null)
            {
                return Result<int>.Failure(
                   ResultStatus.NotFound,
                    "الحركة غير موجودة.");
            }

            _unitOfWork.MotionsForInformation.Delete(motion);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                motion.Id,
                "تم حذف الحركة بنجاح.");
        }
    }
}