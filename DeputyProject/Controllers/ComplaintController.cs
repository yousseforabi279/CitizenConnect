using Application.Common;
using Application.Core.Commands.AddcommentToRequest;
using Application.Core.Commands.CreateCompliant;
using Application.Core.Commands.DeleteCitizenRequest;
using Application.Core.Commands.NewFolder;
using Application.Core.Queries.CitizenRequests.GetRequestById;
using Azure.Core;
using Bank.Api.Controllers;
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
        [HttpPost(ApiRoutes.Complaint.CreateComplaint)]
        public async Task<IActionResult> CreateComplaint([FromForm] CreateCitizenRequest Request)
        {
            var command = new CreateCompliantCommand
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
        [Authorize]

        [HttpGet(ApiRoutes.Complaint.GetComplaintById)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(
                new GetCitizenRequestByIdQuery(id));
            return HandleResult(result);

        }
        [Authorize]
        [HttpDelete(ApiRoutes.Complaint.DeleteComplaintById)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(
                new DeleteCitizenRequestCommand(id));
            return HandleResult(result);
        }
        [Authorize]
        [HttpPost(ApiRoutes.Complaint.Addcomment)]
        public async Task<IActionResult> AddComment(
                        int requestId,
                        [FromBody] AddCommentRequest request)
        {
                var command = new AddCommentCommand
                {
                    CitizinRequiermentId = requestId,
                    Comment = request.Comment
                };

            var result = await _mediator.Send(command);

            return HandleResult(result);

        }
        [Authorize]

        [HttpPatch(ApiRoutes.Complaint.changestatus)]
        public async Task<IActionResult> ChangeStatus(
                    int id,
                    ChangeRequestStatusCommand command)
        {
            command.CitizinRequiermentId = id;
            var result = await _mediator.Send(command);
            return HandleResult(result);

        }

    }
}
