using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.ForgetPassword.ForgetPass
{
    namespace Application.Core.Commands.ForgotPassword
    {
        public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
        {
            public ForgotPasswordCommandValidator()
            {
                RuleFor(x => x.Email)
                      .NotEmpty().WithMessage("البريد الإلكتروني مطلوب.")
                      .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة.");
            }
        }
    }
}
