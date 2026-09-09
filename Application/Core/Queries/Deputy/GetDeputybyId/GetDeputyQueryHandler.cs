using Application.Common;
using Application.Contracts;
using Application.storage;
using MediatR;

namespace Application.Core.Queries.Deputy.GetDeputybyId
{
    internal class GetDeputyQueryHandler
        : IRequestHandler<GetDeputyQuery, Result<DeputyResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "personal-deputy-files";

        public GetDeputyQueryHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<DeputyResponse>> Handle(
            GetDeputyQuery request,
            CancellationToken cancellationToken)
        {
            var deputy =
                await _unitOfWork.Deputy
                    .GetDeputyInfo();

            if (deputy is null)
            {
                return Result<DeputyResponse>.Failure(
                    ResultStatus.NotFound,
                    "النائب غير موجود.");
            }

            var response = new DeputyResponse
            {
                FullName = deputy.FullName,
                BirthOfdate = deputy.BirthOfdate,
                PrimaryPhone = deputy.PrimaryPhone,
                SecondaryPhone = deputy.SecondaryPhone,
                Address = deputy.Address,
                Title = deputy.Title,
                Bio = deputy.Bio,
                AboutPart1 = deputy.AboutPart1,
                AboutPart2 = deputy.AboutPart2,
                OfficeLocation = deputy.OfficeLocation,
                WhatsApp = deputy.WhatsApp,
                FacebookLing = deputy.FacebookLing,
                LocationURL = deputy.LocationURL,
                Circle = deputy.Circle,
                Appointment = deputy.Appointment,

                MediaUrl = string.IsNullOrWhiteSpace(deputy.BlobName)
                    ? null
                    : _fileStorageService.GetFileUrl(
                        deputy.BlobName,
                        FolderName),

                ContentType = deputy.ContentType,
                MediaType = deputy.MediaType
            };

            return Result<DeputyResponse>.Success(
                response,
                "تم جلب بيانات النائب بنجاح.");
        }
    }
}