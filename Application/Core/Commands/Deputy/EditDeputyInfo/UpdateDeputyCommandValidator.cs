using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.EditDeputyInfo
{
    public class UpdateDeputyCommandValidator
       : AbstractValidator<UpdateDeputyCommand>
    {
        public UpdateDeputyCommandValidator()
        {

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("الاسم مطلوب.")
                .MaximumLength(150)
                .WithMessage("الاسم يجب ألا يتجاوز 150 حرف.");

            RuleFor(x => x.BirthOfdate)
                .LessThan(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("تاريخ الميلاد غير صحيح.");

            RuleFor(x => x.PrimaryPhone)
                .NotEmpty()
                .WithMessage("رقم الهاتف الأساسي مطلوب.")
                .MaximumLength(20)
                .WithMessage("رقم الهاتف غير صحيح.");

            RuleFor(x => x.SecondaryPhone)
                .MaximumLength(20)
                .WithMessage("رقم الهاتف الإضافي غير صحيح.")
                .When(x => !string.IsNullOrWhiteSpace(x.SecondaryPhone));

            RuleFor(x => x.Address)
                .MaximumLength(300)
                .WithMessage("العنوان يجب ألا يتجاوز 150 حرف.")
                .When(x => !string.IsNullOrWhiteSpace(x.Address));

            RuleFor(x => x.Title)
                .MaximumLength(150)
                .WithMessage("المسمى الوظيفي يجب ألا يتجاوز 150 حرف.")
                .When(x => !string.IsNullOrWhiteSpace(x.Title));

            RuleFor(x => x.FacebookLing)
                .Must(BeValidUrl)
                .WithMessage("رابط Facebook غير صحيح.")
                .When(x => !string.IsNullOrWhiteSpace(x.FacebookLing));

            RuleFor(x => x.LocationURL)
                .Must(BeValidUrl)
                .WithMessage("رابط الموقع غير صحيح.")
                .When(x => !string.IsNullOrWhiteSpace(x.LocationURL));

            RuleFor(x => x.WhatsApp)
                .MaximumLength(20)
                .WithMessage("رقم WhatsApp غير صحيح.")
                .When(x => !string.IsNullOrWhiteSpace(x.WhatsApp));

            RuleFor(x => x.Circle)
                .MaximumLength(150)
                .WithMessage("الدائرة يجب ألا يتجاوز 150 حرف.")
                .When(x => !string.IsNullOrWhiteSpace(x.Circle));

            RuleFor(x => x.Appointment)
                .MaximumLength(200)
                .WithMessage("مواعيد التواصل طويلة جدًا.")
                .When(x => !string.IsNullOrWhiteSpace(x.Appointment));
        }

        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(
                url,
                UriKind.Absolute,
                out _);
        }
    }
}
