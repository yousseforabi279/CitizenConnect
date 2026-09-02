using Application.Common;
using Application.Contracts;
using Domain.Deputy;
using MediatR;

public class CreateMotionCommandHandler
    : IRequestHandler<CreateMotionCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMotionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(
        CreateMotionCommand request,
        CancellationToken cancellationToken)
    {
        var motion = new MotionsForInformation
        {
            Title = request.Title,
            Description = request.Description,
            Image_Video = request.Image_Video
        };

        await _unitOfWork.MotionsForInformation.AddAsync(motion);
        await _unitOfWork.SaveChangesAsync();
        return Result<int>.Success(
            motion.Id,
            "تم إضافة الطلب بنجاح.");
    }
}