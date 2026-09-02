using Application.Core.Commands.Deputy.achievements.CreateAchievement;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.DeputyWords.CreateDeputyWords
{
    public class CreateDeputyWordsCommandValidator : AbstractValidator<CreateDeputyWordsCommand>
    {
        public CreateDeputyWordsCommandValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("عنوان الإنجاز مطلوب.")
                .MaximumLength(200)
                .WithMessage("عنوان الإنجاز لا يمكن أن يتجاوز 200 حرف.");

            
            RuleFor(x => x.Image)
                .NotEmpty()
                .WithMessage("صورة الإنجاز مطلوبة.");
        }
    }
}
