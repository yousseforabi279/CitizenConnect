using Application.Common;
using Application.Core.Queries.Employee.GetEmployeeInfo;
using Application.Core.Queries.Employee.GetEmployeeRequestStatistics;
using Application.Core.Queries.GetAllEmployeeonLendingPage;
using Application.Core.Queries.GetRequestsForEmplyees;
using Application.Core.Queries.Me;
using DeputyProject.Controllers;
using DeputyProject.Common;
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
        [Authorize(Roles ="Employee")]
        [HttpGet(ApiRoutes.Employee.GetEmployee)]
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
        [Authorize]
        [HttpGet(ApiRoutes.Employee.statistics)]
        public async Task<IActionResult> GetEmployeeRequestStatistics()
        {
            var result = await _mediator.Send(new GetEmployeeRequestStatisticsQuery());
            return HandleResult(result);
        }

        [HttpGet("GeneralInfo")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await _mediator.Send(new GetCurrentUserQuery());
            return HandleResult(result);

        }
        [HttpGet("AllEmployee")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _mediator.Send(
                new GetAllEmployeesQuery());

            return HandleResult(result);

        }
    }
}
