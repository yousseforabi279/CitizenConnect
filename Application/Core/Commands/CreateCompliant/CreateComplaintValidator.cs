using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.CreateCompliant
{
    public class CreateComplaintValidator
    : AbstractValidator<CreateCompliantCommand>
    {
        public CreateComplaintValidator()
        {
            RuleFor(x => x.CitizenName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.NationalId)
                .NotEmpty()
                .Length(14);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0);

            RuleFor(x => x.Priority)
                .IsInEnum();
        }
    }
}
