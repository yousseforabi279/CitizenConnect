using Application.Core.Queries.Employee.GetEmployeeInfo;
using Application.Core.Queries.Employee.GetEmployeeRequestStatistics;
using Application.Core.Queries.GetRequestsForEmplyees;
using Bank.Api.Controllers;
using DeputyProject.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]

    public class EmployeeController : BaseController
    {
        public EmployeeController(IMediator _mediator) : base(_mediator) { }
        [Authorize(Roles ="Employee")]
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
        [Authorize]
        [HttpGet(ApiRoutes.Employee.statistics)]
        public async Task<IActionResult> GetEmployeeRequestStatistics()
        {
            var result = await _mediator.Send(new GetEmployeeRequestStatisticsQuery());
            return HandleResult(result);
        }
    }
}
