using Application.Core.Commands.CreateCompliant;
using Bank.Api.Controllers;
using DeputyProject.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : BaseController
    {
        public ComplaintController(IMediator _mediator) : base(_mediator) { }
        [HttpPost(ApiRoutes.Complaint.CreateComplaint)]
        public async Task<IActionResult> CreateComplaint(CreateCompliantCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

    }
}
