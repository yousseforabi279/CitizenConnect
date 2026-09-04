using Application.Common;
using Application.storage;
using MediatR;

namespace Application.Core.Commands.LoadingPage.MotionsForInformation.EditMotionsForInformation
{
    public class EditMotionCommand : IRequest<Result<MotionsForInformationDTO>>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public FileUploadRequest? Media { get; set; } // null = keep existing media
    }
}