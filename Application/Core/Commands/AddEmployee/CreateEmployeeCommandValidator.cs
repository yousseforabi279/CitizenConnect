using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.AddEmployee
{
    public class CreateEmployeeCommandValidator
    : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0);
            RuleFor(x => x.PhoneNumber)
    .NotEmpty()
    .Matches(@"^01[0125][0-9]{8}$") // adjust to your country's format
    .WithMessage("Invalid phone number.");
        }
    }
}
