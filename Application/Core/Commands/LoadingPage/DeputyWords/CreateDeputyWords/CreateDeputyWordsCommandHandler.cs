using Application.Common;
using Application.Contracts;
using Application.Core.Commands.Deputy.achievements.CreateAchievement;
using Application.storage;
using Domain.Deputy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.DeputyWords.CreateDeputyWords
{
    internal class CreateDeputyWordsCommandHandler
        : IRequestHandler<CreateDeputyWordsCommand, Result<DeputyWordsDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "deputy-words-files";

        public CreateDeputyWordsCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<DeputyWordsDTO>> Handle(
            CreateDeputyWordsCommand request,
            CancellationToken cancellationToken)
        {
            var upload = await _blobStorageService.UploadFileAsync(request.Media, ContainerName);

            var word = new Domain.Deputy.DeputyWords
            {
                Title = request.Title,
                BlobName = upload.BlobName,
                MediaFileName = request.Media.FileName,
                ContentType = upload.ContentType,
                FileSizeBytes = upload.SizeBytes,
                MediaType = request.Media.ContentType.StartsWith("video") ? MediaType.Video : MediaType.Image,
                UploadedAt = DateTime.UtcNow
            };

            await _unitOfWork.Deputyword.AddAsync(word);
            await _unitOfWork.SaveChangesAsync();

            var dto = new DeputyWordsDTO
            {
                Id = word.Id,
                Title = word.Title,
                MediaUrl = _blobStorageService.GetReadSasUrl(word.BlobName, ContainerName),
                ContentType = word.ContentType,
                MediaType = word.MediaType
            };

            return Result<DeputyWordsDTO>.Success(dto, "تمت إضافة كلمة النائب بنجاح.");
        }
    }
}