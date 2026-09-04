using Application.Common;
using Application.Contracts;
using Application.Core.Commands.Deputy.ActivityVisit.EditActivityVisit;
using Application.Core.Commands.LoadingPage.ActivityVisit;
using Application.storage;
using Domain.Deputy;
using MediatR;

internal class UpdateActivityVisitCommandHandler
       : IRequestHandler<UpdateActivityVisitCommand, Result<ActivityVisitDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;
    private const string ContainerName = "activity-visit-files";

    public UpdateActivityVisitCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
    {
        _unitOfWork = unitOfWork;
        _blobStorageService = blobStorageService;
    }

    public async Task<Result<ActivityVisitDTO>> Handle(
        UpdateActivityVisitCommand request,
        CancellationToken cancellationToken)
    {
        var activity = await _unitOfWork.ActitvitiesAndVisits.GetByIdAsync(request.Id);
        if (activity == null)
            return Result<ActivityVisitDTO>.Failure(ResultStatus.NotFound,"النشاط غير موجود.");

        if (request.Media != null)
        {
            if (!string.IsNullOrEmpty(activity.BlobName))
                await _blobStorageService.DeleteFileAsync(activity.BlobName, ContainerName);

            var upload = await _blobStorageService.UploadFileAsync(request.Media, ContainerName);

            activity.BlobName = upload.BlobName;
            activity.MediaFileName = request.Media.FileName;
            activity.ContentType = upload.ContentType;
            activity.FileSizeBytes = upload.SizeBytes;
            activity.MediaType = request.Media.ContentType.StartsWith("video")
                ? MediaType.Video
                : MediaType.Image;
            activity.UploadedAt = DateTime.UtcNow;
        }

        activity.Title = request.Title;
        activity.Description = request.Description;
        activity.Location = request.Location;
        activity.Date = request.Date;

        await _unitOfWork.SaveChangesAsync();

        var dto = new ActivityVisitDTO
        {
            Id = activity.Id,
            Title = activity.Title,
            Description = activity.Description,
            Location = activity.Location,
            Date = activity.Date,
            MediaUrl = activity.BlobName != null
                ? _blobStorageService.GetReadSasUrl(activity.BlobName, ContainerName)
                : null,
            ContentType = activity.ContentType,
            MediaType = activity.MediaType
        };

        return Result<ActivityVisitDTO>.Success(dto, "تم تعديل النشاط أو الزيارة بنجاح.");
    }

}