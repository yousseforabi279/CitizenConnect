using Application.Common;
using Application.Contracts;
using Application.Core.Commands.ForgetPassword.GenerateNumericCode;
using Application.Core.Commands.ForgetPassword.Resetpassword;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICodeGeneratorService _codeGenerator;

        public ResetPasswordCommandHandler(IUnitOfWork unitOfWork, ICodeGeneratorService codeGenerator)
        {
            _unitOfWork = unitOfWork;
            _codeGenerator = codeGenerator;
        }

        public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.IdentityService.FindByEmailAsync(request.Email);
            if (user is null)
                return Result<string>.Failure(ResultStatus.BadRequest, "Invalid or expired code.");

            var resetCode = await _unitOfWork.PasswordResetCode.GetLatestValidAsync(user.Id);
            if (resetCode is null || !_codeGenerator.Verify(request.Code, resetCode.CodeHash))
                return Result<string>.Failure(ResultStatus.BadRequest, "Invalid or expired code.");

            // Reset the password directly through Identity (needs its own token internally,
            // OR use a "remove + add password" approach — see note below)
            var resetResult = await _unitOfWork.IdentityService
                .ResetPasswordDirectAsync(user, request.NewPassword);

            if (!resetResult.Succeeded)
                return Result<string>.Failure(ResultStatus.BadRequest, string.Join(" ", resetResult.Errors));

            // Mark this code (and any other pending ones) as used, and kill existing sessions
            await _unitOfWork.PasswordResetCode.InvalidateAllForUserAsync(user.Id);
            await _unitOfWork.RefreshToken.RevokeAllForUserAsync(user.Id);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Password has been reset successfully.", "Password has been reset successfully.");
        }
    }
}