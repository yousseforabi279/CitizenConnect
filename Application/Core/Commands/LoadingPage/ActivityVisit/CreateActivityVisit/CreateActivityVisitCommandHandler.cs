using Application.Common;
using Application.Contracts;
using Domain.Deputy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.ActivityVisit.CreateActivityVisit
{
    internal class CreateActivityVisitCommandHandler
    : IRequestHandler<CreateActivityVisitCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateActivityVisitCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            CreateActivityVisitCommand request,
            CancellationToken cancellationToken)
        {
           
            var activity = new ActitvitiesAndVisits
            {
                Title = request.Title,
                Description = request.Description,
                Image_Video = request.Image_Video,
                Location = request.Location,
                Date = request.Date
            };

            //await _unitOfWork.ActitvitiesAndVisits.AddAsync(activity);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                1,
                "تمت إضافة النشاط أو الزيارة بنجاح.");
        }
    }
}
