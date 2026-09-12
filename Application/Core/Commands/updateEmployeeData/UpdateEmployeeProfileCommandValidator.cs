using Application.Core.Commands.updateEmployee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Login
{
    public class UpdateEmployeeProfileCommandValidator : AbstractValidator<UpdateEmployeeProfileCommand>
    {
        public UpdateEmployeeProfileCommandValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotNull().WithMessage("ال Id مطلوب");
        }
    }
}
