using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

    [HttpPost]
    public async Task<IActionResult> Create(OrderItemDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(new ApiResponseDto<OrderItemResponseDto>
        {
            Message = "Order item created successfully",
            Data = result
        });
    }

    [HttpPut("{orderItemId}")]
    public async Task<IActionResult> Update(int orderItemId, OrderItemDto dto)
    {
        var result = await _service.UpdateAsync(orderItemId, dto);

        return Ok(new ApiResponseDto<OrderItemResponseDto>
        {
            Message = "Order item updated successfully",
            Data = result
        });
    }

    [HttpGet("{orderItemId}")]
    public async Task<IActionResult> Get(int orderItemId)
    {
        var result = await _service.GetByIdAsync(orderItemId);

        return Ok(new ApiResponseDto<OrderItemResponseDto>
        {
            Message = "Order item retrieved successfully",
            Data = result
        });
    }

    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetByOrder(int orderId)
    {
        var result = await _service.GetByOrderIdAsync(orderId);

        return Ok(new ApiResponseDto<List<OrderItemResponseDto>>
        {
            Message = "Order items retrieved successfully",
            Data = result
        });
    }

    [HttpDelete("{orderItemId}")]
    public async Task<IActionResult> Delete(int orderItemId)
    {
        await _service.DeleteAsync(orderItemId);

        return Ok(new ApiResponseDto<object>
        {
            Message = "Order item deleted successfully",
            Data = null
        });
    }
}