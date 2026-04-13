using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/laborders")]
[Authorize(Roles = "Receptionist,Admin")]
public class LabOrderController : ControllerBase
{
    private readonly ILabOrderService _service;

    public LabOrderController(ILabOrderService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] LabOrderDto dto)
    {
        if (dto == null)
            return BadRequest(new { message = "Request body cannot be null." });

        var result = await _service.CreateAsync(dto);

        return Ok(new
        {
            message = "Lab order created successfully",
            data = result
        });
    }

    [HttpGet("list")]
    public async Task<IActionResult> Get(
         [FromQuery] int? patientId,
         [FromQuery] DateTime? orderDate)
    {
        var result = await _service.GetAsync(patientId, orderDate);

        return Ok(new
        {
            message = "Lab orders retrieved successfully",
            data = result
        });
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetById(int orderId)
    {
        var result = await _service.GetByIdAsync(orderId);

        return Ok(new
        {
            message = "Lab order retrieved successfully",
            data = result
        });
    }

    [HttpPut("{orderId}")]
    public async Task<IActionResult> Update(int orderId, [FromBody] LabOrderDto dto)
    {
        if (dto == null)
            return BadRequest(new { message = "Request body cannot be null." });

        var result = await _service.UpdateAsync(orderId, dto);

        return Ok(new
        {
            message = "Lab order updated successfully",
            data = result
        });
    }

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> Delete(int orderId)
    {
        await _service.DeleteAsync(orderId);

        return Ok(new
        {
            message = "Lab order deleted successfully"
        });
    }
}