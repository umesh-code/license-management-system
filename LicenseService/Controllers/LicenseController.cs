using LicenseService.Commands;
using LicenseService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LicenseService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LicenseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LicenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateLicenseCommand command)
        {
            if (command == null)
                return BadRequest("Invalid request");

            var id = await _mediator.Send(command);

            return Ok(new
            {
                message = "License created successfully",
                licenseId = id
            });
        }

        [HttpGet("{tenantId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get(string tenantId)
        {
            var result = await _mediator.Send(new GetLicensesQuery(tenantId));
            return Ok(result);
        }
    }
}