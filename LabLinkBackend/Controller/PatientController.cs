using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/patients")]
[Authorize]
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

        var userIdClaim = User.FindFirst("userId");

        if (userIdClaim == null)
            return Unauthorized("UserId claim not found in token.");

        if (!int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized("Invalid UserId claim in token.");

        var patient = new Patient
        {
            PatientId = dto.PatientId ?? 0,
            UserId = userId,
            Name = dto.Name,
            Dob = dto.Dob,
            Gender = dto.Gender,
            ContactInfo = dto.ContactInfo,
            Address = dto.Address,
            IsActive = dto.IsActive,
            PrimaryPhysicianName = dto.PrimaryPhysicianName
        };

        var result = await _service.UpsertPatientAsync(patient);

        return Ok(new PatientResponseDto
        {
            PatientId = result.PatientId,
            UserId = result.UserId,
            Name = result.Name,
            Dob = result.Dob,
            Gender = result.Gender,
            ContactInfo = result.ContactInfo,
            Address = result.Address,
            IsActive = result.IsActive,
            PrimaryPhysicianName = result.PrimaryPhysicianName
        });
    }
}