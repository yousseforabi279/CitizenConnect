using Application.Common;
using Application.Contracts;
using Application.Core.Commands.Deputy.achievements.DeleteAchievement;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.DeputyWords.DeleteDeputyWords
{
    internal class DeleteDeputyWordsCommandHandler
    : IRequestHandler<DeleteDeputyWordCommend, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "deputy-words-files";

        public DeleteDeputyWordsCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<int>> Handle(
            DeleteDeputyWordCommend request,
            CancellationToken cancellationToken)
        {
            var word = await _unitOfWork.Deputyword.GetByIdAsync(request.DeputyWordId);
            if (word is null)
            {
                return Result<int>.Failure(ResultStatus.NotFound, "كلمة النائب غير موجودة.");
            }

            if (!string.IsNullOrEmpty(word.BlobName))
                await _blobStorageService.DeleteFileAsync(word.BlobName, ContainerName);

            _unitOfWork.Deputyword.Delete(word);
            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(word.Id, "تم الحذف بنجاح.");
        }
    }
}
