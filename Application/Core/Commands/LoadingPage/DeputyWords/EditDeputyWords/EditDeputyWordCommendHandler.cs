using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.DeputyWords.CreateDeputyWords;
using Application.storage;
using Domain.Deputy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.DeputyWords.EditDeputyWords
{
    internal class UpdateDeputyWordsCommandHandler
     : IRequestHandler<UpdateDeputyWordsCommand, Result<DeputyWordsDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "deputy-words-files";

        public UpdateDeputyWordsCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<DeputyWordsDTO>> Handle(
            UpdateDeputyWordsCommand request,
            CancellationToken cancellationToken)
        {
            var word = await _unitOfWork.Deputyword.GetByIdAsync(request.Id);
            if (word is null)
            {
                return Result<DeputyWordsDTO>.Failure(ResultStatus.NotFound, "كلمة النائب غير موجودة.");
            }

            if (request.Media != null)
            {
                if (!string.IsNullOrEmpty(word.BlobName))
                    await _blobStorageService.DeleteFileAsync(word.BlobName, ContainerName);

                var upload = await _blobStorageService.UploadFileAsync(request.Media, ContainerName);

                word.BlobName = upload.BlobName;
                word.MediaFileName = request.Media.FileName;
                word.ContentType = upload.ContentType;
                word.FileSizeBytes = upload.SizeBytes;
                word.MediaType = request.Media.ContentType.StartsWith("video") ? MediaType.Video : MediaType.Image;
                word.UploadedAt = DateTime.UtcNow;
            }

            word.Title = request.Title;

            _unitOfWork.Deputyword.Update(word);
            await _unitOfWork.SaveChangesAsync();

            var dto = new DeputyWordsDTO
            {
                Id = word.Id,
                Title = word.Title,
                MediaUrl = _blobStorageService.GetReadSasUrl(word.BlobName, ContainerName),
                ContentType = word.ContentType,
                MediaType = word.MediaType
            };

            return Result<DeputyWordsDTO>.Success(dto, "تم تعديل كلمة النائب بنجاح.");
        }
    }
}
