using Application.Common;
using Application.Core.Commands.CreateComplaint;
using Application.Core.Commands.DeleteCitizenRequest;
using Application.Core.Commands.UpdateRequestStatus;
using Application.Core.Queries.CitizenRequests.GetRequestById;
using DeputyProject.Controllers;
using DeputyProject.Common;
using DeputyProject.Mappers;
using DeputyProject.Requests.CitizenRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : BaseController
    {
        public ComplaintController(IMediator _mediator) : base(_mediator) { }

        // Citizens file complaints without an account.
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Complaint.CreateComplaint)]
        public async Task<IActionResult> CreateComplaint([FromForm] CreateCitizenRequest Request)
        {
            var command = new CreateComplaintCommand
            {
                BirthDate = Request.BirthDate,
                DepartmentId = Request.DepartmentId,
                Description = Request.Description,
                FullName = Request.FullName,
                NationalId = Request.NationalId,
                OrganizationId = Request.OrganizationId,
                Phone = Request.Phone,
                RequestType = Request.RequestType,
                Title = Request.Title,
                Media = Request.Image.MapToFileUploadRequest()
            };
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [Authorize(Roles = "Employee")]
        [HttpGet(ApiRoutes.Complaint.GetComplaintById)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(
                new GetCitizenRequestByIdQuery(id));
            return HandleResult(result);

        }
        [Authorize(Roles = "Employee")]
        [HttpDelete(ApiRoutes.Complaint.DeleteComplaintById)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(
                new DeleteCitizenRequestCommand(id));
            return HandleResult(result);
        }
        [Authorize(Roles = "Employee")]
        [HttpPut(ApiRoutes.Complaint.UpdateRequest)]
        public async Task<IActionResult> UpdateRequest([FromBody] UpdateRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

    }
}
