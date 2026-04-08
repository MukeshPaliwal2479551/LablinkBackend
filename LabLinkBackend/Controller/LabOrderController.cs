using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
    public async Task<IActionResult> Create(LabOrderDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(new ApiResponseDto<LabOrderResponseDto>
        {
            Message = "Lab order created successfully",
            Data = result
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(new ApiResponseDto<List<LabOrderResponseDto>>
        {
            Message = "Lab orders retrieved successfully",
            Data = result
        });
    }

    [HttpPut("{orderId}")]
    public async Task<IActionResult> Update(int orderId, LabOrderDto dto)
    {
        var result = await _service.UpdateAsync(orderId, dto);

        return Ok(new ApiResponseDto<LabOrderResponseDto>
        {
            Message = "Lab order updated successfully",
            Data = result
        });
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> Get(int orderId)
    {
        var result = await _service.GetByIdAsync(orderId);

        return Ok(new ApiResponseDto<LabOrderResponseDto>
        {
            Message = "Lab order retrieved successfully",
            Data = result
        });
    }

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> Delete(int orderId)
    {
        await _service.DeleteAsync(orderId);

        return Ok(new ApiResponseDto<object>
        {
            Message = "Lab order deleted successfully",
            Data = null
        });
    }

}