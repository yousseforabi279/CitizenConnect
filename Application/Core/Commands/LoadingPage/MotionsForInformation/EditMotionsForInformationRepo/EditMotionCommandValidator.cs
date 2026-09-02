using FluentValidation;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.EditMotionsForInformation
{
    public class EditMotionCommandValidator
        : AbstractValidator<EditMotionCommand>
    {
        public EditMotionCommandValidator()
        {
            RuleFor(x => x.MotionId)
                .GreaterThan(0)
                .WithMessage("رقم الطلب غير صحيح.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("العنوان مطلوب.")
                .MaximumLength(200)
                .WithMessage("العنوان لا يمكن أن يتجاوز 200 حرف.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("الوصف مطلوب.")
                .MaximumLength(1000)
                .WithMessage("الوصف لا يمكن أن يتجاوز 1000 حرف.");

            RuleFor(x => x.Image_Video)
                .NotEmpty()
                .WithMessage("الصورة أو الفيديو مطلوب.");
        }
    }
}