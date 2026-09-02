using Application.Common;
using Application.Contracts;
using Application.Core.Commands.Deputy.achievements.CreateAchievement;
using Domain.Deputy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.DeputyWords.CreateDeputyWords
{
    internal class CreateDeputyWordsCommandHandler : IRequestHandler<CreateDeputyWordsCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDeputyWordsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            CreateDeputyWordsCommand request,
            CancellationToken cancellationToken)
        {
            var deputyWords = new Domain.Deputy.DeputyWords
            {
                Title = request.Title,
                Video_image = request.Image,
            };
            await _unitOfWork.Deputyword.AddAsync(deputyWords);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                deputyWords.Id,
                "تم إضافة الإنجاز بنجاح.");


        }
    }
}