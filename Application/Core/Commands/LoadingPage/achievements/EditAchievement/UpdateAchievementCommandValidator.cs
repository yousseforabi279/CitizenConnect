using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace Application.Core.Commands.Deputy.achievements.EditAchievement
{
    public class UpdateAchievementCommandValidator
        : AbstractValidator<UpdateAchievementCommand>
    {
        public UpdateAchievementCommandValidator()
        {

            RuleFor(x => x.AchievementId)
                .GreaterThan(0)
                .WithMessage("الإنجاز غير صحيح.");

            RuleFor(x => x.Title)
                .MaximumLength(200)
                .WithMessage("عنوان الإنجاز لا يمكن أن يتجاوز 200 حرف.");

            RuleFor(x => x.Media)
                 .Must(m => m == null || m.Length <= 50_000_000) // 50MB cap example
                 .WithMessage("File must be under 50MB");
        }
    }
}
