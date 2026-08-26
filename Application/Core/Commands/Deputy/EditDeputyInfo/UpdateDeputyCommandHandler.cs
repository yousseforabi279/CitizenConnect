using Application.Common;
using Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.EditDeputyInfo
{
    internal class UpdateDeputyCommandHandler
       : IRequestHandler<UpdateDeputyCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDeputyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(
               UpdateDeputyCommand request,
               CancellationToken cancellationToken)
        {
            var deputy = await _unitOfWork.Deputy
                .GetByIdAsync(request.Id);

            if (deputy is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "النائب غير موجود.");
            }

            deputy.FullName = request.FullName;
            deputy.BirthOfdate = request.BirthOfdate;
            deputy.PrimaryPhone = request.PrimaryPhone;
            deputy.SecondaryPhone = request.SecondaryPhone;
            deputy.Address = request.Address;
            deputy.Title = request.Title;
            deputy.Bio = request.Bio;
            deputy.AboutPart1 = request.AboutPart1;
            deputy.AboutPart2 = request.AboutPart2;
            deputy.OfficeLocation = request.OfficeLocation;
            deputy.WhatsApp = request.WhatsApp;
            deputy.FacebookLing = request.FacebookLing;
            deputy.LocationURL = request.LocationURL;
            deputy.Circle = request.Circle;
            deputy.Appointment = request.Appointment;

            _unitOfWork.Deputy.Update(deputy);

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(
                deputy.Id,
                "تم التعديل بنجاح");
        }
    }
}