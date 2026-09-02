using Application.Common;
using MediatR;

public class CreateMotionCommand : IRequest<Result<int>>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Image_Video { get; set; }
}