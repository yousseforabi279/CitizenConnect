using Bank.Api.Controllers;
using DeputyProject.SeedDataDto;
using Domain;
using Infrastructure.Dbcontext;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedDataController : BaseController
    {
        protected readonly Appcontext _context;
        public SeedDataController(IMediator _mediator, Appcontext context) : base(_mediator) { _context = context; }
        [HttpPost]
        public async Task<IActionResult> AddDeputy([FromBody] CreateDeputyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deputy = new Domain.Deputy.Deputy
            {
                FullName = dto.FullName,
                BirthOfdate = dto.BirthOfdate,
                PrimaryPhone = dto.PrimaryPhone,
                SecondaryPhone = dto.SecondaryPhone,
                Address = dto.Address,
                Title = dto.Title,
                Bio = dto.Bio,
                AboutPart1 = dto.AboutPart1,
                AboutPart2 = dto.AboutPart2,
                OfficeLocation = dto.OfficeLocation,
                WhatsApp = dto.WhatsApp,
                FacebookLing = dto.FacebookLing,
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

            return CreatedAtAction(nameof(AddDeputy), new { id = deputy.Id }, deputy);
        }
        [HttpPost("AddOrganization")]
        public async Task<IActionResult> AddOrganization([FromBody] CreateOrganizationDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required.");

            var org = new Organization { Name = dto.Name };

            _context.Organizations.Add(org);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddOrganization), new { id = org.Id }, org);
        }
        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] CreateDepartmentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required.");

            var dept = new Department { Name = dto.Name };

            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddDepartment), new { id = dept.Id }, dept);
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
