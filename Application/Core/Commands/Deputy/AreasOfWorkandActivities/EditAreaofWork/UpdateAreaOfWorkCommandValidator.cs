using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.EditAreaofWork
{
    public class UpdateAreaOfWorkCommandValidator
     : AbstractValidator<UpdateAreaOfWorkCommand>
    {
        public UpdateAreaOfWorkCommandValidator()
        {
            RuleFor(x => x.DeputyId)
                .GreaterThan(0)
                .WithMessage("النائب غير صحيح.");

            RuleFor(x => x.AreaId)
                .GreaterThan(0)
                .WithMessage("مجال العمل غير صحيح.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("عنوان مجال العمل مطلوب.")
                .MaximumLength(200)
                .WithMessage("العنوان لا يمكن أن يتجاوز 200 حرف.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("وصف مجال العمل مطلوب.");

            RuleFor(x => x.Image)
                .NotEmpty()
                .WithMessage("الصورة مطلوبة.");
        }
    }
}
