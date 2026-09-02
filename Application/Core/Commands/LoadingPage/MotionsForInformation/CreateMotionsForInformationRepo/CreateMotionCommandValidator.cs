using FluentValidation;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.CreateMotionsForInformationRepo
{
    public class CreateMotionCommandValidator
        : AbstractValidator<CreateMotionCommand>
    {
        public CreateMotionCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("عنوان الحركة مطلوب.")
                .MaximumLength(200)
                .WithMessage("عنوان الحركة لا يمكن أن يتجاوز 200 حرف.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("وصف الحركة مطلوب.")
                .MaximumLength(1000)
                .WithMessage("وصف الحركة لا يمكن أن يتجاوز 1000 حرف.");

            RuleFor(x => x.Image_Video)
                .NotEmpty()
                .WithMessage("صورة أو فيديو الحركة مطلوب.");
        }
    }
}

