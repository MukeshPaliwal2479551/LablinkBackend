using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/results")]
[Authorize]
public class ResultEntryController : ControllerBase
{
    private readonly IResultEntryService _service;

    public ResultEntryController(IResultEntryService service)
    {
        _service = service;
    }

    /// <summary>
    /// POST /api/results
    /// Manually enter a new result. Status is set to "Draft" server-side.
    /// EnteredBy is resolved from the JWT token.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ResultEntryCreateDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return StatusCode(201, new { message = "Result entry created successfully.", data = result });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to create result entry.", detail = ex.Message });
        }
    }

    /// <summary>
    /// GET /api/results/order/{orderItemId}
    /// Retrieve all results for a specific order item.
    /// </summary>
    [HttpGet("order/{orderItemId:int}")]
    public async Task<IActionResult> GetByOrderItemId(int orderItemId)
    {
        try
        {
            var results = await _service.GetByOrderItemIdAsync(orderItemId);
            return Ok(new { data = results });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to retrieve results.", detail = ex.Message });
        }
    }

    /// <summary>
    /// PUT /api/results/{id}
    /// Update an existing result entry.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ResultEntryCreateDto dto)
    {
        try
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(new { message = "Result entry updated successfully.", data = result });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to update result entry.", detail = ex.Message });
        }
    }
}
