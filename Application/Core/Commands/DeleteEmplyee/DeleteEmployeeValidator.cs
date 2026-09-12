using Application.Core.Commands.ForgetPassword.ForgetPass;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.DeleteEmplyee
{
    internal class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeValidator()
        {
            RuleFor(x => x.EmployeeId)
                  .NotEmpty().WithMessage("ال ID  مطلوب.")
                  .NotNull().WithMessage("ال ID  مطلوب.");
        }
    }
}
