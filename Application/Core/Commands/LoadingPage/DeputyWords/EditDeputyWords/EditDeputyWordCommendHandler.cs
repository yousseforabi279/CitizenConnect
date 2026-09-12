using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.DeputyWords.CreateDeputyWords;
using Application.storage;
using Domain.Deputy;
using MediatR;

namespace Application.Core.Commands.LoadingPage.DeputyWords.EditDeputyWords
{
    internal class UpdateDeputyWordsCommandHandler
        : IRequestHandler<UpdateDeputyWordsCommand, Result<DeputyWordsDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "deputy-words-files";

        public UpdateDeputyWordsCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<DeputyWordsDTO>> Handle(
            UpdateDeputyWordsCommand request,
            CancellationToken cancellationToken)
        {
            var word =
                await _unitOfWork.Deputyword
                    .GetByIdAsync(request.Id);

            if (word is null)
            {
                return Result<DeputyWordsDTO>.Failure(
                    ResultStatus.NotFound,
                    "كلمة النائب غير موجودة.");
            }

            // Replace old media if a new media file was uploaded
            if (request.Media != null)
            {
                if (!string.IsNullOrEmpty(word.BlobName))
                {
                    await _fileStorageService.DeleteFileAsync(
                        word.BlobName,
                        FolderName);
                }

                var upload =
                    await _fileStorageService.UploadFileAsync(
                        request.Media,
                        FolderName);

                word.BlobName = upload.BlobName;
                word.MediaFileName = request.Media.FileName;
                word.ContentType = upload.ContentType;
                word.FileSizeBytes = upload.SizeBytes;

                word.MediaType =
                    request.Media.ContentType.StartsWith("video/")
                        ? MediaType.Video
                        : MediaType.Image;

                word.UploadedAt = DateTime.UtcNow;

            }

            word.Title = request.Title;

            _unitOfWork.Deputyword.Update(word);

            await _unitOfWork.SaveChangesAsync();

            var dto = new DeputyWordsDTO
            {
                Id = word.Id,
                Title = word.Title,

                MediaUrl = !string.IsNullOrEmpty(word.BlobName)
                    ? _fileStorageService.GetFileUrl(
                        word.BlobName,
                        FolderName,word.ContentType)
                    : null,

                ContentType = word.ContentType,
                MediaType = word.MediaType
            };

            return Result<DeputyWordsDTO>.Success(
                dto,
                "تم تعديل كلمة النائب بنجاح.");
        }
    }
}