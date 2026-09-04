using Application.Common;
using Application.Core.Commands.LoadingPage.DeputyWords.CreateDeputyWords;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.DeputyWords.EditDeputyWords
{
    public class EditDeputyWordCommendValidetor : AbstractValidator<UpdateDeputyWordsCommand>
    {
        public EditDeputyWordCommendValidetor()
        {

            RuleFor(x => x.Id)
              .GreaterThan(0).WithMessage("كلمة النائب غير صحيحة.");

            RuleFor(x => x.Title)
                .MaximumLength(200).WithMessage("العنوان لا يمكن أن يتجاوز 200 حرف.");

            When(x => x.Media != null, () =>
            {
                RuleFor(x => x.Media!.Length)
                    .GreaterThan(0).WithMessage("الملف المرفوع غير صالح.");
            });
        }
    }
}
