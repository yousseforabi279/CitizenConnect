using Application.Common;
using MediatR;

namespace Application.Core.Commands.ChangePassword
{
    public record ChangePasswordCommand(
        string CurrentPassword,
        string NewPassword,
        string ConfirmNewPassword
    ) : IRequest<Result<string>>;
}