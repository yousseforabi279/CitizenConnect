using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler
        : IRequestHandler<ChangePasswordCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public ChangePasswordCommandHandler(
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<string>> Handle(
            ChangePasswordCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            if (string.IsNullOrEmpty(userId))
            {
                return Result<string>.Failure(
                    ResultStatus.Unauthorized,
                    "يجب تسجيل الدخول لتغيير كلمة المرور.");
            }

            var user = await _unitOfWork.IdentityService.FindByIdAsync(userId);

            if (user is null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "المستخدم غير موجود.");
            }

            var currentPasswordValid = await _unitOfWork.IdentityService
                .CheckPasswordAsync(user, request.CurrentPassword);

            if (!currentPasswordValid)
            {
                return Result<string>.Failure(
                    ResultStatus.BadRequest,
                    "كلمة المرور الحالية غير صحيحة.");
            }

            var changeResult = await _unitOfWork.IdentityService
                .ChangePasswordAsync(
                    user,
                    request.CurrentPassword,
                    request.NewPassword);

            if (!changeResult.Succeeded)
            {
                return Result<string>.Failure(
                    ResultStatus.BadRequest,
                    string.Join(" ", changeResult.Errors));
            }

            // إلغاء جميع Refresh Tokens الحالية للمستخدم
            // لإجبار الجلسات الأخرى على تسجيل الدخول مرة أخرى.
            await _unitOfWork.RefreshToken.RevokeAllForUserAsync(user.Id);

            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(
                "تم تغيير كلمة المرور بنجاح.",
                "تم تغيير كلمة المرور بنجاح.");
        }
    }
}
