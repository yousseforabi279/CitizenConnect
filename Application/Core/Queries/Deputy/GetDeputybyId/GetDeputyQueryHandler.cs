using Application.Common;
using Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.GetDeputybyId
{
    internal class GetDeputyQueryHandler
        : IRequestHandler<GetDeputyQuery, Result<DeputyResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDeputyQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DeputyResponse>> Handle(
            GetDeputyQuery request,
            CancellationToken cancellationToken)
        {
            var deputy = await _unitOfWork.Deputy
                .GetByIdAsync(request.Id);

            if (deputy is null)
            {
                return Result<DeputyResponse>.Failure(
                    ResultStatus.NotFound,
                    "النائب غير موجود.");
            }

            var response = new DeputyResponse
            {
                Id = deputy.Id,
                FullName = deputy.FullName,
                BirthOfdate = deputy.BirthOfdate,
                PrimaryPhone = deputy.PrimaryPhone,
                SecondaryPhone = deputy.SecondaryPhone,
                Address = deputy.Address,
                Title = deputy.Title,
                Bio = deputy.Bio,
                About = deputy.About,
                WhatsApp = deputy.WhatsApp,
                FacebookLing = deputy.FacebookLing,
                LocationURL = deputy.LocationURL,
                Circle = deputy.Circle,
                Appointment = deputy.Appointment
            };

            return Result<DeputyResponse>.Success(
                response,
                "تم جلب بيانات النائب بنجاح.");
        }
    }
}
