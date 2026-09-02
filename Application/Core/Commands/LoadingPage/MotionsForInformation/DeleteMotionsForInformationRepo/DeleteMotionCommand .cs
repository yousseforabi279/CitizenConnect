using Application.Common;
using MediatR;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.DeleteMotionsForInformation
{
    public class DeleteMotionCommand : IRequest<Result<int>>
    {
        public int MotionId { get; set; }

        public DeleteMotionCommand(int motionId)
        {
            MotionId = motionId;
        }
    }
}