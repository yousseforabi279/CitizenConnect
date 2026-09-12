using Application.Common;
using Application.Core.Commands.AddEmployee;
using Application.Core.Commands.DeleteEmplyee;
using Application.Core.Commands.updateEmployee;
using Application.Core.Queries.Employee.GetEmployeeInfo;
using Application.Core.Queries.Employee.GetEmployeeRequestStatistics;
using Application.Core.Queries.GetAllEmployeeonLendingPage;
using Application.Core.Queries.GetRequestsForEmplyees;
using Application.Core.Queries.Me;
using Application.storage;
using Bank.Api.Controllers;
using DeputyProject.Common;
using DeputyProject.Requests.Employee;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        public EmployeeController(IMediator _mediator) : base(_mediator) { }
        [Authorize]
        [HttpGet(ApiRoutes.Employee.GetEmplyee)]
        public async Task<IActionResult> GetEmployeeInfo()
        {
            var result = await _mediator.Send(new GetEmployeeInfoQuery());
            return HandleResult(result);
        }
        [Authorize(Roles = "Employee")]
        [HttpGet(ApiRoutes.Employee.GetRequestsForEmployees)]
        public async Task<IActionResult> GetEmployeeRequests([FromQuery] GetEmployeeRequestsQuery query)
        {
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }
        [Authorize(Roles = "Employee")]
        [HttpGet(ApiRoutes.Employee.statistics)]
        public async Task<IActionResult> GetEmployeeRequestStatistics()
        {
            var result = await _mediator.Send(new GetEmployeeRequestStatisticsQuery());
            return HandleResult(result);
        }

        [HttpGet("AllEmployee")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _mediator.Send(
                new GetAllEmployeesQuery());

            return HandleResult(result);

        }

        [HttpPost(ApiRoutes.Employee.CreateEmplyee)]
        public async Task<IActionResult> CreateEmplyee([FromForm] CreateEmployeeRequest request)
        {
            FileUploadRequest? imageRequest = null;

            if (request.Image is not null && request.Image.Length > 0)
            {
                imageRequest = new FileUploadRequest
                {
                    Content = request.Image.OpenReadStream(),
                    FileName = request.Image.FileName,
                    ContentType = request.Image.ContentType,
                    Length = request.Image.Length
                };
            }

            var command = new CreateEmployeeCommand(
             request.FullName,
             request.Email,
             request.Password,
             request.Role,
             request.DepartmentId,
             request.OrganizationId,
             request.PhoneNumber,
             request.About,
             imageRequest);

            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpPut(ApiRoutes.Employee.UpdateMyProfile)]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> updateMyProfile([FromForm] UpdateEmployeeProfileRequest request)
        {
            FileUploadRequest? imageRequest = null;

            if (request.Image is not null && request.Image.Length > 0)
            {
                imageRequest = new FileUploadRequest
                {
                    Content = request.Image.OpenReadStream(),
                    FileName = request.Image.FileName,
                    ContentType = request.Image.ContentType,
                    Length = request.Image.Length
                };
            }

            var command = new UpdateEmployeeProfileCommand(
                 request.EmployeeId,
                 request.fullname,
                 request.About,
                 request.Phone,
                 request.DepartmentId,
                 request.OrganizationIds,
                 imageRequest
             );

            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpDelete(ApiRoutes.Employee.DeleteEmployee)]
        public async Task<IActionResult> DeleteEmployee(int EmployeeId)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand { EmployeeId= EmployeeId });
            return HandleResult(result);
        }
    }
}       
