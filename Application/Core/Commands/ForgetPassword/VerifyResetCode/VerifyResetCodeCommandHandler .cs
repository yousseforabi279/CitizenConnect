using Application.Common;
using Application.Contracts;
using Application.Core.Commands.ForgetPassword.GenerateNumericCode;
using Application.Core.Commands.ForgetPassword.NewFolder;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.VerifyResetCode
{
    public class VerifyResetCodeCommandHandler : IRequestHandler<VerifyResetCodeCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICodeGeneratorService _codeGenerator;

        public VerifyResetCodeCommandHandler(IUnitOfWork unitOfWork, ICodeGeneratorService codeGenerator)
        {
            _unitOfWork = unitOfWork;
            _codeGenerator = codeGenerator;
        }

        public async Task<Result<string>> Handle(VerifyResetCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.IdentityService.FindByEmailAsync(request.Email);
            if (user is null)
                return Result<string>.Failure(ResultStatus.BadRequest, "Invalid or expired code.");

            var resetCode = await _unitOfWork.PasswordResetCode.GetLatestValidAsync(user.Id);
            if (resetCode is null || !_codeGenerator.Verify(request.Code, resetCode.CodeHash))
                return Result<string>.Failure(ResultStatus.BadRequest, "Invalid or expired code.");

            return Result<string>.Success("Code verified.", "Code verified.");
        }
    }
}