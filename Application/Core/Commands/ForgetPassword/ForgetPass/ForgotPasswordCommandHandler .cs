using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Commands.ForgetPassword.GenerateNumericCode;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.ForgetPassword.ForgetPass
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICodeGeneratorService _codeGenerator;

        public ForgotPasswordCommandHandler(IUnitOfWork unitOfWork, ICodeGeneratorService codeGenerator)
        {
            _unitOfWork = unitOfWork;
            _codeGenerator = codeGenerator;
        }

        public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.IdentityService.FindByEmailAsync(request.Email);

            if (user is not null)
            {
                var code = _codeGenerator.GenerateNumericCode(6); // e.g. "483920"

                var resetCode = new PasswordResetCode
                {
                    UserId = user.Id,
                    CodeHash = _codeGenerator.Hash(code),
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(10)
                };

                await _unitOfWork.PasswordResetCode.AddAsync(resetCode);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.EmailService.SendPasswordResetCodeAsync(request.Email, code);
            }

            // same message either way — don't reveal if email exists
            return Result<string>.Success("If that email exists, a code has been sent.", "If that email exists, a code has been sent.");
        }
    }
}
