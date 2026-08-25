using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Commands.CreateCompliant.Validation;
using AutoMapper;
using Domain;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.CreateCompliant
{
    public class CreateCompliantCommandHandler : IRequestHandler<CreateCompliantCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INationalIdValidator _nationalId;


        public CreateCompliantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, INationalIdValidator nationalId)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _nationalId = nationalId;
        }

        public async Task<Result<string>> Handle(CreateCompliantCommand request, CancellationToken cancellationToken)
        {
            if (!_nationalId.IsValid(request.NationalId))
            {
                return Result<string>.Failure(
                    ResultStatus.ValidationError,
                    "Invalid national ID.");
            }
            var department = await _unitOfWork.Department.GetByIdAsync(request.DepartmentId);
            if (department is null) 
            {   
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Complaint or Suggestion category not found.");
            }
            var Organization = await _unitOfWork.Organization.GetByIdAsync(request.OrganizationId);
            if (Organization is null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Complaint or Suggestion direction not found.");
            }
            var citizen = await _unitOfWork.Citizin.GetByNationalidAsync(request.NationalId);
           
            if (citizen is null)
            {
                citizen = new Citizen
                {
                    NationalId = request.NationalId,
                    FullName = request.FullName,
                    BirthDate = request.BirthDate,
                    Phone = request.Phone
                };
                await _unitOfWork.Citizin.AddAsync(citizen);
            }

            var requirement = new CitizinRequierment
            {
                Type = request.RequestType,
                Title = request.Title,
                Description = request.Description,
                CreatedAt=DateTime.UtcNow,
                Citizen=citizen,
                Status=RequestStatus.New,
            };
            var Employees = await _unitOfWork.Employee.GetAvailableEmployeesAsync(request.DepartmentId, request.OrganizationId);
            if (Employees is null || !Employees.Any())
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "No employees found for this department and organization.");
            }

            foreach (var employee in Employees)
            {
                requirement.Employees.Add(
                    new CitizinRequiermentEmployee
                    {
                        Employee = employee,
                        CitizinRequierment = requirement
                    });
            }


            await _unitOfWork.CitizinRequierment.AddAsync(requirement);
            await _unitOfWork.SaveChangesAsync();
            return Result<string>.Success("Request created and assigned successfully.");



        }
    }
}
