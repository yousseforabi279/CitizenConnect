using FluentValidation;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.CreateMotionsForInformationRepo
{
    public class CreateMotionCommandValidator
        : AbstractValidator<CreateMotionCommand>
    {
        public CreateMotionCommandValidator()
        {
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

