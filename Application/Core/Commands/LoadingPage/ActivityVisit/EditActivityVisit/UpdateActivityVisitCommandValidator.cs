using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.ActivityVisit.EditActivityVisit
{
    public class UpdateActivityVisitCommandValidator
     : AbstractValidator<UpdateActivityVisitCommand>
    {
        public UpdateActivityVisitCommandValidator()
        {


            RuleFor(x => x.Title)
                .MaximumLength(200)
                .WithMessage("العنوان لا يمكن أن يتجاوز 200 حرف.")
                .When(x => !string.IsNullOrWhiteSpace(x.Title));

            RuleFor(x => x.Description)
                .MaximumLength(2000)
                .WithMessage("الوصف لا يمكن أن يتجاوز 2000 حرف.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));


            RuleFor(x => x.Location)
                .NotEmpty()
                .WithMessage("الموقع مطلوب.")
                .MaximumLength(300)
                .WithMessage("الموقع لا يمكن أن يتجاوز 300 حرف.");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("التاريخ مطلوب.");

            RuleFor(x => x.Media)
            .Must(m => m == null || m.Length <= 50_000_000) // 50MB cap example
            .WithMessage("File must be under 50MB");
        }
    }
}
