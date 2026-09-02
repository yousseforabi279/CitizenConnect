using Application.Common;
using Application.Contracts;
using MediatR;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.EditMotionsForInformation
{
    public class EditMotionCommandHandler
        : IRequestHandler<EditMotionCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditMotionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            EditMotionCommand request,
            CancellationToken cancellationToken)
        {
            var motion = await _unitOfWork.MotionsForInformation
                .GetByIdAsync(request.MotionId);

            if (motion == null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "الطلب غير موجود.");
            }

            motion.Title = request.Title;
            motion.Description = request.Description;
            motion.Image_Video = request.Image_Video;

            _unitOfWork.MotionsForInformation.Update(motion);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                motion.Id,
                "تم تعديل البيانات بنجاح.");
        }
    }
}