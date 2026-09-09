using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.AddcommentToRequest
{
    public class AddCommentValidator : AbstractValidator<AddCommentCommand>
    {
        public AddCommentValidator()
        {
            RuleFor(x => x.CitizinRequiermentId)
                .GreaterThan(0)
                .WithMessage("Request ID must be greater than 0.");


            RuleFor(x => x.Comment)
                .NotEmpty()
                .WithMessage("Comment is required.")
                .MaximumLength(2000)
                .WithMessage("Comment cannot exceed 2000 characters.");
        }
    }
}
