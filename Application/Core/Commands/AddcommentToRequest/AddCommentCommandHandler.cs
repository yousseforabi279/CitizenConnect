using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.AddcommentToRequest
{
    public class AddCommentCommandHandler
       : IRequestHandler<AddCommentCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public AddCommentCommandHandler(
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<string>> Handle(
            AddCommentCommand request,
            CancellationToken cancellationToken)
        {
            var citizenRequest =
                await _unitOfWork.CitizinRequierment
                    .GetByIdAsync(request.CitizinRequiermentId);

            if (citizenRequest == null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Citizen request not found.");
            }

            var employee = await _unitOfWork.Employee
                     .GetByUserIdAsync(_currentUser.UserId);

            if (employee == null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Employee not found.");
            }
            var comment = new CitizinRequiermentContent
            {
                CitizinRequiermentId = request.CitizinRequiermentId,
                EmployeeId = employee.Id,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.CitizinRequiermentContent
                .AddAsync(comment);

            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(
                "Comment added successfully.");
        }
    }
}
