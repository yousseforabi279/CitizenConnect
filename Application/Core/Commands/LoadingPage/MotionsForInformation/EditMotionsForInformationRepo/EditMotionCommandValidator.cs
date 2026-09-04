using FluentValidation;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.EditMotionsForInformation
{
    public class EditMotionCommandValidator
        : AbstractValidator<EditMotionCommand>
    {
        public EditMotionCommandValidator()
        {
            RuleFor(x => x.Id)
             .GreaterThan(0).WithMessage("الطلب الاستعلامي غير صحيح.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("العنوان مطلوب.")
                .MaximumLength(200).WithMessage("العنوان لا يمكن أن يتجاوز 200 حرف.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("الوصف مطلوب.");

            When(x => x.Media != null, () =>
            {
                RuleFor(x => x.Media!.Length)
                    .GreaterThan(0).WithMessage("الملف المرفوع غير صالح.");
            });
        }
    }
}