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
               .WithMessage("Citizen name is required.")
               .MaximumLength(100)
               .WithMessage("Citizen name cannot exceed 100 characters.");

            RuleFor(x => x.NationalId)
                .NotEmpty()
                .WithMessage("National ID is required.")
                .Length(14)
                .WithMessage("National ID must be 14 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .MaximumLength(20)
                .WithMessage("Phone number cannot exceed 20 characters.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Complaint title is required.")
                .MaximumLength(200)
                .WithMessage("Complaint title cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Complaint description is required.")
                .MaximumLength(2000)
                .WithMessage("Complaint description cannot exceed 2000 characters.");

            RuleFor(x => x.CategoryId)
               .GreaterThan(0)
               .WithMessage("Category is required.");

            RuleFor(x => x.Priority)
                .IsInEnum()
                .WithMessage("Invalid complaint priority.");
        }
    }
}
