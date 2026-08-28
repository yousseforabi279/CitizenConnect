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

            RuleFor(x => x.ActivityVisitId)
                .GreaterThan(0)
                .WithMessage("النشاط أو الزيارة غير صحيح.");

            RuleFor(x => x.Title)
                .MaximumLength(200)
                .WithMessage("العنوان لا يمكن أن يتجاوز 200 حرف.")
                .When(x => !string.IsNullOrWhiteSpace(x.Title));

            RuleFor(x => x.Description)
                .MaximumLength(2000)
                .WithMessage("الوصف لا يمكن أن يتجاوز 2000 حرف.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.Image_Video)
                .MaximumLength(500)
                .WithMessage("رابط الصورة أو الفيديو غير صحيح.")
                .When(x => !string.IsNullOrWhiteSpace(x.Image_Video));

            RuleFor(x => x.Location)
                .NotEmpty()
                .WithMessage("الموقع مطلوب.")
                .MaximumLength(300)
                .WithMessage("الموقع لا يمكن أن يتجاوز 300 حرف.");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("التاريخ مطلوب.");
        }
    }
}
