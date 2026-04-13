using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/patients")]
[Authorize(Roles = "Receptionist,Admin,Patient")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _service;

    public PatientController(IPatientService service)
    {
        _service = service;
    }

    [HttpPost("upsert")]
    public async Task<IActionResult> Upsert([FromBody] PatientUpsertDto dto)
    {
        if (dto == null)
            return BadRequest(new { message = "Request body cannot be null." });

        if (!dto.IsCreate && (!dto.PatientId.HasValue || dto.PatientId <= 0))
            return BadRequest(new { message = "Valid PatientId is required for update." });

        var result = await _service.UpsertPatientAsync(dto);

        var message = dto.IsCreate
            ? "Patient created successfully."
            : "Patient updated successfully.";

        return Ok(new
        {
            message,
            data = result
        });
    }

    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetById(int patientId)
    {
        if (patientId <= 0)
            return BadRequest(new { message = "patientId must be greater than 0." });

        var result = await _service.GetByIdAsync(patientId);

        if (result == null)
            return NotFound(new { message = "Patient not found." });

        return Ok(new
        {
            message = "Patient retrieved successfully.",
            data = result
        });
    }

    [HttpGet("list")]
    public async Task<IActionResult> Get(
        [FromQuery] string? name,
        [FromQuery] string? phone)
    {
        if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(phone))
        {

        }

        var result = await _service.GetAsync(name, phone);

        return Ok(new
        {
            message = "Patients retrieved successfully.",
            data = result
        });
    }

    [HttpDelete("{patientId:int:min(1)}")]
    public async Task<IActionResult> Delete(int patientId)
    {
        if (patientId <= 0)
            return BadRequest(new { message = "patientId must be greater than 0." });

        await _service.DeleteAsync(patientId);

        return Ok(new
        {
            message = "Patient deleted successfully."
        });
    }
}