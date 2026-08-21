using Application.Common;
using Application.Contracts;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.CreateCompliant
{
    public class CreateCompliantCommandHandler : IRequestHandler<CreateCompliantCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCompliantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateCompliantCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork
                             .ComplaintCategory
                             .GetByIdAsync(request.CategoryId);
            if (category is null)
            {
                return Result<int>.Failure(
                    ResultStatus.NotFound,
                    "Complaint category not found.");
            }
            var complaint = _mapper.Map<Complaint>(request);
            await _unitOfWork.Complaints.AddAsync(complaint);
            await _unitOfWork.SaveChangesAsync();
            return Result<int>.Success(
                complaint.Id,
                "Complaint created successfully.");
        }
    }
}
