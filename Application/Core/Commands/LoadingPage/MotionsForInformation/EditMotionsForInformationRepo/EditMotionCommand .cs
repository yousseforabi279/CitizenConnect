using Application.Common;
using MediatR;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.EditMotionsForInformation
{
    public class EditMotionCommand : IRequest<Result<int>>
    {
        public int MotionId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Image_Video { get; set; }
    }
}