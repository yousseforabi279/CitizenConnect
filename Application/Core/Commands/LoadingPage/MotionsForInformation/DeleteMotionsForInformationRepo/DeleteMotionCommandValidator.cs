using FluentValidation;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.DeleteMotionsForInformation
{
    public class DeleteMotionCommandValidator
        : AbstractValidator<DeleteMotionCommand>
    {
        public DeleteMotionCommandValidator()
        {
            RuleFor(x => x.MotionId)
                .GreaterThan(0)
                .WithMessage("رقم الحركة غير صحيح.");
        }
    }
}