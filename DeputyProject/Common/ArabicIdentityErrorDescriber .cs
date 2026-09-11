using Microsoft.AspNetCore.Identity;

namespace DeputyProject.Common
{
    public class ArabicIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
            => new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = $"اسم المستخدم '{userName}' مستخدم بالفعل."
            };

        public override IdentityError DuplicateEmail(string email)
            => new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = $"البريد الإلكتروني '{email}' مستخدم بالفعل."
            };

        public override IdentityError PasswordTooShort(int length)
            => new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = $"كلمة المرور يجب أن تكون {length} أحرف على الأقل."
            };

        public override IdentityError PasswordRequiresNonAlphanumeric()
            => new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "كلمة المرور يجب أن تحتوي على رمز غير أبجدي رقمي واحد على الأقل."
            };

        public override IdentityError PasswordRequiresDigit()
            => new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "كلمة المرور يجب أن تحتوي على رقم واحد على الأقل ('0'-'9')."
            };

        public override IdentityError PasswordRequiresUpper()
            => new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "كلمة المرور يجب أن تحتوي على حرف كبير واحد على الأقل ('A'-'Z')."
            };

        public override IdentityError PasswordRequiresLower()
            => new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "كلمة المرور يجب أن تحتوي على حرف صغير واحد على الأقل ('a'-'z')."
            };

        public override IdentityError InvalidEmail(string email)
            => new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = $"البريد الإلكتروني '{email}' غير صالح."
            };

        public override IdentityError InvalidUserName(string userName)
            => new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = $"اسم المستخدم '{userName}' غير صالح، يمكن أن يحتوي فقط على أحرف وأرقام."
            };

        // Override any other methods you need (UserAlreadyInRole, DefaultError, etc.)
    }
}
