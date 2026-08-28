using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.ActivityVisit.DeleteActivityVisit
{
    public class DeleteActivityVisitCommandValidator
        : AbstractValidator<DeleteActivityVisitCommand>
    {
        public DeleteActivityVisitCommandValidator()
        {
            RuleFor(x => x.ActivityVisitId)
                .GreaterThan(0)
                .WithMessage("النشاط أو الزيارة غير صحيح.");
        }
    }
}
