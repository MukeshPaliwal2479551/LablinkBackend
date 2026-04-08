using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LabLinkBackend.DTO;
using LabLinkBackend.Services;

namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuditController : ControllerBase
{
    private readonly IAuditLogService _auditLogService;

    public AuditController(IAuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }
    
    [HttpPost]   
    public async Task<ActionResult<AuditLogResult>> CreateAudit([FromBody] AuditDto AuditInfo)
    {
        var userIdString = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized(new { error = "Invalid user ID in token" });
        }

        AuditInfo.UserId = userId;
        var res = await _auditLogService.CreateLogAsync(AuditInfo);
        return Ok(res);
    }
}
