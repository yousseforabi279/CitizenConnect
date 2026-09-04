using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.achievements.CreateAchievement
{
    public class CreateAchievementCommandValidator
      : AbstractValidator<CreateAchievementCommand>
    {
        public CreateAchievementCommandValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("عنوان الإنجاز مطلوب.")
                .MaximumLength(200)
                .WithMessage("عنوان الإنجاز لا يمكن أن يتجاوز 200 حرف.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("وصف الإنجاز مطلوب.");
            RuleFor(x => x.Media)
            .Must(m => m == null || m.Length <= 50_000_000) // 50MB cap example
            .WithMessage("File must be under 50MB");
        }
    }
}
