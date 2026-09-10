using Application.Common;
using Application.Contracts;
using Application.storage;
using MediatR;

namespace Application.Core.Commands.LoadingPage.DeputyWords.DeleteDeputyWords
{
    internal class DeleteDeputyWordsCommandHandler
        : IRequestHandler<DeleteDeputyWordCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "deputy-words-files";

        public DeleteDeputyWordsCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<int>> Handle(
            DeleteDeputyWordCommand request,
            CancellationToken cancellationToken)
        {
            var word =
                await _unitOfWork.DeputyWord
                    .GetByIdAsync(request.DeputyWordId);

            if (word is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "كلمة النائب غير موجودة.");
            }

            if (!string.IsNullOrEmpty(word.BlobName))
            {
                await _fileStorageService.DeleteFileAsync(
                    word.BlobName,
                    FolderName);
            }

            _unitOfWork.DeputyWord.Delete(word);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                word.Id,
                "تم الحذف بنجاح.");
        }
    }
}