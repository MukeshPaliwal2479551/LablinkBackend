using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/appointments")]
[Authorize(Roles="Patient,Recipient")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _service;

    public AppointmentController(IAppointmentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AppointmentDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(new { message = "Appointment created successfully.", data = result });
    }

    [HttpPut("{appointmentId}")]
    public async Task<IActionResult> Update(int appointmentId, AppointmentDto dto)
    {
        var result = await _service.UpdateAsync(appointmentId, dto);
        return Ok(new { message = "Appointment updated successfully.", data = result });
    }

    [HttpDelete("{appointmentId}")]
    public async Task<IActionResult> Delete(int appointmentId)
    {
        await _service.DeleteAsync(appointmentId);
        return Ok(new { message = "Appointment deleted successfully." });
    }

    [HttpGet("{appointmentId}")]
    public async Task<IActionResult> GetById(int appointmentId)
    {
        var result = await _service.GetByIdAsync(appointmentId);
        return Ok(new { data = result });
    }

    [HttpGet]
    public async Task<IActionResult> GetByDate([FromQuery] DateOnly? date)
    {
        var result = await _service.GetByDateAsync(date);
        return Ok(new { data = result });
    }
}
