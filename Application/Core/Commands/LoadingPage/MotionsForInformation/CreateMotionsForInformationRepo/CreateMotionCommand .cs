using Application.Common;
using Application.Core.Commands.LoadingPage.MotionsForInformation;
using Application.storage;
using MediatR;

public class CreateMotionCommand : IRequest<Result<MotionsForInformationDTO>>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public FileUploadRequest? Media { get; set; }
}