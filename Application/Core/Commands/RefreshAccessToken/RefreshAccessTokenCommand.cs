using Application.Common;
using Application.Core.Commands.Login;
using MediatR;

namespace Application.Core.Commands.RefreshAccessToken
{
    public record RefreshAccessTokenCommand(
        string RefreshToken
    ) : IRequest<Result<LoginResponse>>;
}
