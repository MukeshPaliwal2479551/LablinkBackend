using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/instruments")]
[Authorize(Roles = "Admin, Lab Technologist")]
public class InstrumentRefController : ControllerBase
{
    private readonly IInstrumentRefService _service;

    public InstrumentRefController(IInstrumentRefService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? instrumentId, [FromQuery] string? name, [FromQuery] string? section)
    {
        if (instrumentId.HasValue)
        {
            var result = await _service.GetByIdAsync(instrumentId.Value);
            if (result.Success)
                return Ok(result.Data);
            return NotFound(new { error = result.Error });
        }
        else
        {
            var result = await _service.GetAllAsync(name, section);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(new { error = result.Error });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InstrumentRefDto dto)
    {
        var result = await _service.UpsertAsync(dto);
        if (!result.Success)
            return BadRequest(new { error = result.Error });

       var message = "Instrument created successfully.";

        return Ok(new { message, data = result.Data });
    }

    [HttpPut("{instrumentId}")]
    public async Task<IActionResult> UpdateDetails(int instrumentId, [FromBody] InstrumentRefDto dto)
    {
        var result = await _service.UpdateDetailsAsync(instrumentId, dto);
        if (!result.Success)
            return BadRequest(new { error = result.Error });

        return Ok(new { message = "Instrument updated successfully.", data = result.Data });
    }

    [HttpDelete("{instrumentId}")]
    public async Task<IActionResult> Delete(int instrumentId)
    {
        var result = await _service.DeleteAsync(instrumentId);
        if (!result.Success)
            return NotFound(new { error = result.Error });

        return Ok(new { message = "Instrument deactivated successfully." });
    }
}
