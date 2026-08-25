using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Commands.CreateCompliant.Validation;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.Core.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public LoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, INationalIdValidator nationalId)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.IdentityService
                       .FindByEmailAsync(request.Email);
            if (user is null)
            {
                return Result<LoginResponse>.Failure(
                    ResultStatus.NotFound,
                    "Invalid email or password.");
            }
            var passwordValid = await _unitOfWork.IdentityService
                .CheckPasswordAsync(
                    user,
                    request.Password);
            if (!passwordValid)
            {
                return Result<LoginResponse>.Failure(
                     ResultStatus.NotFound,
                    "Invalid email or password.");
            }

            var roles = await _unitOfWork.IdentityService.GetRolesAsync(user);
            var accessToken =_unitOfWork.jwtTokenService.GenerateAccessToken(user,roles);
            var refreshTokenValue =_unitOfWork.jwtTokenService.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                Token = refreshTokenValue,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
            await _unitOfWork.RefreshToken.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();   

            return Result<LoginResponse>.Success(
                   new LoginResponse
                   {
                       AccessToken = accessToken,
                       RefreshToken = refreshTokenValue
                   },
                   "Login successful.");


        }
    }
}
