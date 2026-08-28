using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.DeleteAreaOfWork
{
    public class DeleteAreaOfWorkCommandValidator
      : AbstractValidator<DeleteAreaOfWorkCommand>
    {
        public DeleteAreaOfWorkCommandValidator()
        {

            RuleFor(x => x.AreaId)
                .GreaterThan(0)
                .WithMessage("مجال العمل غير صحيح.");
        }
    }
}
