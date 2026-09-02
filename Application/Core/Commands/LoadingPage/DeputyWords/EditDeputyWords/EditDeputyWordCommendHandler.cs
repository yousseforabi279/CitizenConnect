using Application.Common;
using Application.Contracts;
using Application.Core.Commands.LoadingPage.DeputyWords.CreateDeputyWords;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.DeputyWords.EditDeputyWords
{
    internal class EditDeputyWordCommendHandler : IRequestHandler<EditDeputyWordCommend, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditDeputyWordCommendHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(EditDeputyWordCommend request, CancellationToken cancellationToken)
        {
            var DeputyWord = await _unitOfWork.Achievement
                 .GetByIdAsync(request.DeputyWordId);

            if (DeputyWord is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "الإنجاز غير موجود.");
            }

            DeputyWord.Title = request.Title;
            DeputyWord.Image = request.Image;

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                DeputyWord.Id,
                "تم تعديل الإنجاز بنجاح.");
        }
    }
}
