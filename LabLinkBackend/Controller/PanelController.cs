using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LabLinkBackend.DTO;
using LabLinkBackend.Services;

namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]  
public class PanelController : ControllerBase
{
    private readonly IPanelService _panelService;

    public PanelController(IPanelService panelService)
    {
        _panelService = panelService;
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreatePanel([FromBody] PanelDto panelInfo)
    {
        var result = await _panelService.CreatePanelAsync(panelInfo);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(new { error = result.Error });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdatePanel([FromBody] PanelDto updatePanelInfo)
    {
        int currentUserId = int.Parse(User.FindFirst("userId")?.Value);
        var result = await _panelService.UpdatePanelAsync(updatePanelInfo, currentUserId);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(new { error = result.Error });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPanels([FromQuery] string? panelName = null, [FromQuery] string? panelCode = null)
    {
        var panels = await _panelService.GetAllPanelsAsync(panelName, panelCode);
        return Ok(panels);
    }

    [HttpDelete("deactivate/{id}")]
    public async Task<IActionResult> DeactivatePanel(int id)
    {
        int currentUserId = int.Parse(User.FindFirst("userId")?.Value);
        
        var result = await _panelService.DeactivatePanelAsync(id, currentUserId);
        
        if (!result.Success)
            return NotFound($"Panel with ID {id} not found.");
            
        return NoContent();
    }
}



