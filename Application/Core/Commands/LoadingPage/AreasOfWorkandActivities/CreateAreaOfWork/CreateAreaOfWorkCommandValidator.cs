using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.CreateAreaOfWork
{
    public class CreateAreaOfWorkCommandValidator
      : AbstractValidator<CreateAreaOfWorkCommand>
    {
        public CreateAreaOfWorkCommandValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("عنوان مجال العمل مطلوب.")
                .MaximumLength(200)
                .WithMessage("العنوان لا يمكن أن يتجاوز 200 حرف.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("وصف مجال العمل مطلوب.");

            RuleFor(x => x.Image)
                .NotNull().WithMessage("صورة مجال العمل مطلوبة.")
                .Must(f => f.Length > 0).WithMessage("الملف المرفوع غير صالح.");
        }
    }
}
