using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    public async Task<IActionResult> Upsert(PatientUpsertDto patientUpsertDto)
    {
        var result = await _service.UpsertPatientAsync(patientUpsertDto);

        var message = patientUpsertDto.IsCreate
               ? "Patient created successfully."
               : "Patient updated successfully.";

        return Ok(new
        {
            message,
            data = result
        });

    }
}