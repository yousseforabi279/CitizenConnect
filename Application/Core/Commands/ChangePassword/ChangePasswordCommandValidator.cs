
using FluentValidation;
namespace Application.Core.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage("كلمة المرور الحالية مطلوبة.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("كلمة المرور الجديدة مطلوبة.")
                .MinimumLength(6)
                .WithMessage("يجب أن تحتوي كلمة المرور الجديدة على 6 أحرف على الأقل.")
                .NotEqual(x => x.CurrentPassword)
                .WithMessage("يجب أن تكون كلمة المرور الجديدة مختلفة عن كلمة المرور الحالية.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                .WithMessage("يرجى تأكيد كلمة المرور الجديدة.")
                .Equal(x => x.NewPassword)
                .WithMessage("كلمتا المرور غير متطابقتين.");
        }
    }
}