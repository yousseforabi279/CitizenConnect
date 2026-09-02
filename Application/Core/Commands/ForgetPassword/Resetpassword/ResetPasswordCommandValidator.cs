using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.ForgetPassword.Resetpassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب.")
                .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("رمز التحقق مطلوب.")
                .Length(6).WithMessage("يجب أن يتكون الرمز من 6 أرقام.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("كلمة المرور الجديدة مطلوبة.")
                .MinimumLength(6).WithMessage("يجب أن تتكون كلمة المرور الجديدة من 6 أحرف على الأقل.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("يرجى تأكيد كلمة المرور الجديدة.")
                .Equal(x => x.NewPassword).WithMessage("كلمتا المرور غير متطابقتين.");
        }
    }
}
