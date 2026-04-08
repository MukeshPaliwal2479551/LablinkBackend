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
        var userIdClaim = User.FindFirst("userId")?.Value;//we need this for audit trail check service layer
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
        {
            return Unauthorized(new { message = "Invalid authentication context" });
        }
        var result = await _panelService.UpdatePanelAsync(updatePanelInfo, currentUserId);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(new { error = result.Error });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPanels()
    {
        var panels = await _panelService.GetAllPanelsAsync();
        return Ok(panels);
    }

    [HttpPut("{id}/deactivate")]
    public async Task<IActionResult> DeactivatePanel(int id)
    {
        var userIdClaim = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
        {
            return Unauthorized(new { message = "Invalid authentication context" });
        }
        var result = await _panelService.DeactivatePanelAsync(id, currentUserId);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(new { error = result.Error });
    }
}



