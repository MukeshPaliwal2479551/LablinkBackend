using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/orderitems")]
[Authorize(Roles = "Receptionist,Admin")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _service;

    public OrderItemController(IOrderItemService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] OrderItemDto dto)
    {
        if (dto == null)
            return BadRequest(new { message = "Request body cannot be null." });

        var result = await _service.CreateAsync(dto);

        if (result == null)
            return StatusCode(500, new
            {
                message = "Order item could not be created."
            });

        return Ok(new
        {
            message = "Order item created successfully",
            data = result
        });
    }


    [HttpPut("{orderItemId}")]
    public async Task<IActionResult> Update(int orderItemId, [FromBody] OrderItemDto dto)
    {
        if (dto == null)
            return BadRequest(new { message = "Request body cannot be null." });

        var result = await _service.UpdateAsync(orderItemId, dto);

        if (result == null)
            return NotFound(new
            {
                message = $"Order item with id {orderItemId} was not found."
            });

        return Ok(new
        {
            message = "Order item updated successfully",
            data = result
        });
    }

    [HttpGet("{orderItemId}")]
    public async Task<IActionResult> GetItem(int orderItemId)
    {
        var result = await _service.GetByIdAsync(orderItemId);

        if (result == null)
            return NotFound(new
            {
                message = $"Order item with id {orderItemId} was not found."
            });

        return Ok(new
        {
            message = "Order item retrieved successfully",
            data = result
        });
    }

    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetByOrder(int orderId)
    {
        var result = await _service.GetByOrderIdAsync(orderId);

        return Ok(new
        {
            message = "Order items retrieved successfully",
            data = result
        });
    }

    [HttpDelete("{orderItemId}")]
    public async Task<IActionResult> Delete(int orderItemId)
    {
        await _service.DeleteAsync(orderItemId);

        return Ok(new
        {
            message = "Order item deleted successfully"
        });
    }
}