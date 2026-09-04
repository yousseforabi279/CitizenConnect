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
             .MaximumLength(200).WithMessage("العنوان لا يمكن أن يتجاوز 200 حرف.");

            RuleFor(x => x.Media)
                .NotNull().WithMessage("الفيديو أو الصورة مطلوبة.")
                .Must(f => f == null || f.Length > 0).WithMessage("الملف المرفوع غير صالح.");
        }
    }
}
