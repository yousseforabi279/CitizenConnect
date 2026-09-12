using Application.Common;
using Application.Contracts;
using Application.Core.Commands.Deputy.achievements.CreateAchievement;
using Application.storage;
using Domain.Deputy;
using MediatR;

namespace Application.Core.Commands.LoadingPage.DeputyWords.CreateDeputyWords
{
    internal class CreateDeputyWordsCommandHandler
        : IRequestHandler<CreateDeputyWordsCommand, Result<DeputyWordsDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "deputy-words-files";

        public CreateDeputyWordsCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<DeputyWordsDTO>> Handle(
            CreateDeputyWordsCommand request,
            CancellationToken cancellationToken)
        {
            var word = new Domain.Deputy.DeputyWords
            {
                Title = request.Title
            };

            if (request.Media != null)
            {
                var upload = await _fileStorageService.UploadFileAsync(
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

            await _unitOfWork.Deputyword.AddAsync(word);

            await _unitOfWork.SaveChangesAsync();

            var dto = new DeputyWordsDTO
            {
                Id = word.Id,
                Title = word.Title,

                MediaUrl = !string.IsNullOrEmpty(word.BlobName)
                    ? _fileStorageService.GetFileUrl(
                        word.BlobName,
                        FolderName, word.ContentType)
                    : null,

                ContentType = word.ContentType,
                MediaType = word.MediaType
            };

            return Result<DeputyWordsDTO>.Success(
                dto,
                "تمت إضافة كلمة النائب بنجاح.");
        }
    }
}