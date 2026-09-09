using Application.Common;
using Application.Contracts;
using Application.Core.Queries.GetAllEmployeeonLendingPage;
using MediatR;

public class GetAllEmployeesQueryHandler
    : IRequestHandler<
        GetAllEmployeesQuery,
        Result<List<EmployeeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllEmployeesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<EmployeeResponse>>> Handle(
        GetAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var employees =
            await _unitOfWork.Employee.GetAllwithUserAsync();

        var result = employees.Select(employee => new EmployeeResponse
        {
            Id = employee.Id,
            Name = employee.User.UserName!,
            Phone = employee.User.PhoneNumber,
            about = employee.about??""
        }).ToList();

        return Result<List<EmployeeResponse>>.Success(result);
    }
}