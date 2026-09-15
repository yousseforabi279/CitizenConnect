using Application.Common;
using Application.Contracts;
using Application.Core.Queries.GetAllEmployeeonLendingPage;
using Application.storage;
using MediatR;

public class GetAllEmployeesQueryHandler
    : IRequestHandler<
        GetAllEmployeesQuery,
        Result<List<EmployeeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;
    private const string FolderName = "employees";
    public GetAllEmployeesQueryHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork; 
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<List<EmployeeResponse>>> Handle(
        GetAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var employees =
            await _unitOfWork.Employee.GetAllwithUserAsync();

        var result = employees.Select(employee => new EmployeeResponse
        {
            Id = employee.UserId,
            FullName = employee.User.FullName!,
            Email = employee.User.Email,
            ImageUrl = !string.IsNullOrEmpty(employee.Image?.BlobName)
                ? _fileStorageService.GetFileUrl(employee.Image!.BlobName, FolderName, employee.Image.ContentType)
                : null,
            about = employee.about ?? "",
            phone = employee.User.PhoneNumber
        }).ToList();
        return Result<List<EmployeeResponse>>.Success(result);
    }
}