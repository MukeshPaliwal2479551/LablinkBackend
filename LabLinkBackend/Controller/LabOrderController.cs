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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LabOrderDto dto)
    {
        if (dto == null)
            return BadRequest(new { message = "Request body cannot be null." });

        var result = await _service.CreateAsync(dto);

        if (result == null)
            return StatusCode(500, new
            {
                message = "Lab order could not be created."
            });

        return Ok(new
        {
            message = "Lab order created successfully",
            data = result
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(new
        {
            message = "Lab orders retrieved successfully",
            data = result
        });
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] LabOrderDto dto)
    {
        if (dto == null)
            return BadRequest(new { message = "Request body cannot be null." });

        var result = await _service.SearchAsync(dto.PatientId, dto.OrderDate);

        return Ok(new
        {
            message = "Lab orders retrieved successfully",
            data = result
        });
    }

    [HttpPut("{orderId:int}")]
    public async Task<IActionResult> Update(int orderId, [FromBody] LabOrderDto dto)
    {
        if (dto == null)
            return BadRequest(new { message = "Request body cannot be null." });

        var result = await _service.UpdateAsync(orderId, dto);

        if (result == null)
            return NotFound(new
            {
                message = $"Lab order with id {orderId} was not found."
            });

        return Ok(new
        {
            message = "Lab order updated successfully",
            data = result
        });
    }

    [HttpGet("{orderId:int}")]
    public async Task<IActionResult> Get(int orderId)
    {
        var result = await _service.GetByIdAsync(orderId);

        if (result == null)
            return NotFound(new
            {
                message = $"Lab order with id {orderId} was not found."
            });

        return Ok(new
        {
            message = "Lab order retrieved successfully",
            data = result
        });
    }

    [HttpDelete("{orderId:int}")]
    public async Task<IActionResult> Delete(int orderId)
    {
        await _service.DeleteAsync(orderId);

        return Ok(new
        {
            message = "Lab order deleted successfully"
        });
    }
}