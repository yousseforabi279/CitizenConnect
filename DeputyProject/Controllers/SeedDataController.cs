using Application.Core.Commands.AddEmployee;
using DeputyProject.Controllers;
using DeputyProject.SeedDataDto;
using Domain;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;

namespace DeputyProject.Controllers
{
    // Seeds initial reference data (deputy profile, organizations, departments,
    // the first employee account) directly against the DbContext, bypassing the
    // normal Application/MediatR pipeline. This is a bootstrap tool only — it is
    // disabled outside the Development environment so it can never be reached
    // in production.
    [Route("api/[controller]")]
    [ApiController]
    public class SeedDataController : BaseController, IActionFilter
    {
        protected readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public SeedDataController(
            IMediator _mediator,
            ApplicationDbContext context,
            IWebHostEnvironment environment) : base(_mediator)
        {
            _context = context;
            _environment = environment;
        }

        [NonAction]
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_environment.IsDevelopment())
            {
                context.Result = NotFound();
            }
        }

        [NonAction]
        public void OnActionExecuted(ActionExecutedContext context) { }

        [HttpPost]
        public async Task<IActionResult> AddDeputy([FromBody] CreateDeputyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deputy = new Domain.Deputy.Deputy
            {
                FullName = dto.FullName,
                BirthDate = dto.BirthDate,
                PrimaryPhone = dto.PrimaryPhone,
                SecondaryPhone = dto.SecondaryPhone,
                Address = dto.Address,
                Title = dto.Title,
                Bio = dto.Bio,
                AboutPart1 = dto.AboutPart1,
                AboutPart2 = dto.AboutPart2,
                OfficeLocation = dto.OfficeLocation,
                WhatsApp = dto.WhatsApp,
                FacebookLink = dto.FacebookLink,
                LocationURL = dto.LocationURL,
                Circle = dto.Circle,
                Appointment = dto.Appointment,

                // Media fields left empty/default — set later via a separate upload endpoint
                BlobName = string.Empty,
                MediaFileName = string.Empty,
                ContentType = string.Empty,
                FileSizeBytes = 0,
                MediaType = Domain.Deputy.MediaType.Image, // adjust to your enum's default value
                UploadedAt = DateTime.UtcNow,
                MediaUrl = string.Empty
            };

            _context.Deputies.Add(deputy);
            await _context.SaveChangesAsync();

            return Ok(deputy);
        }

        [HttpPost("AddOrganization")]
        public async Task<IActionResult> AddOrganization([FromBody] CreateOrganizationDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required.");

            var org = new Organization { Name = dto.Name };

            _context.Organizations.Add(org);
            await _context.SaveChangesAsync();

            return Ok(org);
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] CreateDepartmentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required.");

            var dept = new Department { Name = dto.Name };

            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();

            return Ok(dept);
        }

        // Bootstraps the very first employee account. Registration
        // (AuthenticationController.Register) requires an authenticated
        // employee, so this dev-only endpoint exists to break that chicken-
        // and-egg problem on a fresh database.
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpGet("GetOrganizations")]
        public async Task<IActionResult> GetOrganizations()
        {
            var organizations = await _context.Organizations
                .AsNoTracking()
                .ToListAsync();

            return Ok(organizations);
        }

        [HttpGet("GetOrganization/{id}")]
        public async Task<IActionResult> GetOrganization(int id)
        {
            var organization = await _context.Organizations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (organization is null)
                return NotFound($"Organization with id {id} was not found.");

            return Ok(organization);
        }

        [HttpGet("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _context.Departments
                .AsNoTracking()
                .ToListAsync();

            return Ok(departments);
        }

        [HttpGet("GetDepartment/{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department is null)
                return NotFound($"Department with id {id} was not found.");

            return Ok(department);
        }
    }
}
