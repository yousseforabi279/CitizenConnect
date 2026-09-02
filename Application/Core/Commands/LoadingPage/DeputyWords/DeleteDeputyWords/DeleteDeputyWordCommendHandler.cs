using Application.Common;
using Application.Contracts;
using Application.Core.Commands.Deputy.achievements.DeleteAchievement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.DeputyWords.DeleteDeputyWords
{
    internal class DeleteDeputyWordCommendHandler
      : IRequestHandler<DeleteDeputyWordCommend, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDeputyWordCommendHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            DeleteDeputyWordCommend request,
            CancellationToken cancellationToken)
        {
            var DeputyWord = await _unitOfWork.Achievement
                .GetByIdAsync(request.DeputyWordId);

            if (DeputyWord is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "الإنجاز غير موجود.");
            }

            _unitOfWork.Achievement.Delete(DeputyWord);
            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                DeputyWord.Id,
                "تم حذف الإنجاز بنجاح.");
        }
    }
}
