using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.MotionsForInformation;
using Application.storage;
using Domain.Deputy;
using MediatR;

internal class CreateMotionsForInformationCommandHandler
        : IRequestHandler<CreateMotionCommand, Result<MotionsForInformationDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;
    private const string ContainerName = "motions-for-information-files";

    public CreateMotionsForInformationCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
    {
        _unitOfWork = unitOfWork;
        _blobStorageService = blobStorageService;
    }

    public async Task<Result<MotionsForInformationDTO>> Handle(
        CreateMotionCommand request,
        CancellationToken cancellationToken)
    {
        var motion = new Domain.Deputy.MotionsForInformation
        {
            Title = request.Title,
            Description = request.Description
        };

        if (request.Media != null)
        {
            var upload = await _blobStorageService.UploadFileAsync(request.Media, ContainerName);

            motion.BlobName = upload.BlobName;
            motion.MediaFileName = request.Media.FileName;
            motion.ContentType = upload.ContentType;
            motion.FileSizeBytes = upload.SizeBytes;
            motion.MediaType = request.Media.ContentType.StartsWith("video") ? MediaType.Video : MediaType.Image;
            motion.UploadedAt = DateTime.UtcNow;
        }

        await _unitOfWork.MotionsForInformation.AddAsync(motion);
        await _unitOfWork.SaveChangesAsync();

        var dto = new MotionsForInformationDTO
        {
            Id = motion.Id,
            Title = motion.Title,
            Description = motion.Description,
            MediaUrl = motion.BlobName != null
                ? _blobStorageService.GetReadSasUrl(motion.BlobName, ContainerName)
                : null,
            ContentType = motion.ContentType,
            MediaType = motion.MediaType
        };

        return Result<MotionsForInformationDTO>.Success(dto, "تمت إضافة الطلب الاستعلامي بنجاح.");
    }
}