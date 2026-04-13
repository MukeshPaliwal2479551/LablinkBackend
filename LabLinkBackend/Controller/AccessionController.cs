using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/accessions")]
[Authorize(Roles = "Receptionist,Admin,LabTechnician")]
public class AccessionController : ControllerBase
{
    private readonly IAccessionService _service;

    public AccessionController(IAccessionService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(
    [FromBody] AccessionDto dto,
    [FromServices] IValidator<AccessionDto> validator)
    {
        if (dto == null)
            return BadRequest(new { message = "Request body cannot be null." });

        var validation = await validator.ValidateAsync(
            dto,
            opts => opts.IncludeRuleSets("Create")
        );

        if (!validation.IsValid)
            return BadRequest(validation.Errors);

        var result = await _service.CreateAsync(dto.OrderId, dto.Section);

        if (result == null)
            return NotFound(new
            {
                message = "Lab order not found, inactive, or already accessioned."
            });

        return Ok(new
        {
            message = "Accession created successfully",
            data = result
        });
    }


    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetByOrder(int orderId)
    {
        var result = await _service.GetByOrderIdAsync(orderId);

        if (result == null)
            return NotFound(new
            {
                message = "No accession found for this order."
            });

        return Ok(new
        {
            message = "Accession retrieved successfully",
            data = result
        });
    }

    [HttpPut("{accessionId}/section")]
    public async Task<IActionResult> UpdateSection(
     int accessionId,
     [FromBody] AccessionDto dto,
     [FromServices] IValidator<AccessionDto> validator)
    {
        if (dto == null)
            return BadRequest(new { message = "Request body cannot be null." });

        var validation = await validator.ValidateAsync(
            dto,
            opts => opts.IncludeRuleSets("UpdateSection")
        );

        if (!validation.IsValid)
            return BadRequest(validation.Errors);

        var result = await _service.UpdateSectionAsync(accessionId, dto.Section);

        if (result == null)
            return NotFound(new
            {
                message = "Accession not found or already cancelled."
            });

        return Ok(new
        {
            message = "Accession section updated successfully",
            data = result
        });
    }

    [HttpPut("{accessionId}/cancel")]
    public async Task<IActionResult> Cancel(int accessionId)
    {
        var success = await _service.CancelAsync(accessionId);

        if (!success)
            return NotFound(new
            {
                message = "Accession not found or already cancelled."
            });

        return Ok(new
        {
            message = "Accession cancelled successfully."
        });
    }
}