using Application.Common;
using Application.Contracts;
using Application.Core.Commands.Login;
using Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.RefreshAccessToken
{
    public class RefreshAccessTokenCommandHandler
        : IRequestHandler<RefreshAccessTokenCommand, Result<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RefreshAccessTokenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<LoginResponse>> Handle(
            RefreshAccessTokenCommand request,
            CancellationToken cancellationToken)
        {
            var incomingHash = _unitOfWork.jwtTokenService.HashToken(request.RefreshToken);

            var existingToken = await _unitOfWork.RefreshToken.GetByHashAsync(incomingHash);

            if (existingToken is null
                || existingToken.IsRevoked
                || existingToken.ExpiresAt <= DateTime.UtcNow)
            {
                return Result<LoginResponse>.Failure(
                    ResultStatus.Unauthorized,
                    "Invalid or expired refresh token.");
            }

            existingToken.IsRevoked = true;
            existingToken.RevokedAt = DateTime.UtcNow;
            _unitOfWork.RefreshToken.Update(existingToken);

            var roles = await _unitOfWork.IdentityService.GetRolesAsync(existingToken.User);
            var newAccessToken = _unitOfWork.jwtTokenService.GenerateAccessToken(existingToken.User, roles);
            var newRefreshTokenValue = _unitOfWork.jwtTokenService.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken
            {
                TokenHash = _unitOfWork.jwtTokenService.HashToken(newRefreshTokenValue),
                UserId = existingToken.UserId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.Add(_unitOfWork.jwtTokenService.RefreshTokenLifetime)
            };
            await _unitOfWork.RefreshToken.AddAsync(newRefreshToken);

            await _unitOfWork.SaveChangesAsync();

            return Result<LoginResponse>.Success(
                new LoginResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshTokenValue
                },
                "Token refreshed.");
        }
    }
}
