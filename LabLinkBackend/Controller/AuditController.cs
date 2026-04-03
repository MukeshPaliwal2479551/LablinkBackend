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
    [Route("CreateAudit")]
    public async Task<ActionResult<AuditLogResult>> CreateAudit([FromBody] AuditRequestDto dto)
    {
        var userIdClaim = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
        {
            return Unauthorized(new { message = "Invalid authentication context" });
        }

        var auditDto = new AuditDto
        {
            UserId = currentUserId,
            Action = dto.Action,
            Resource = dto.Resource,
            Metadata = dto.Metadata
        };

        var res = await _auditLogService.CreateLogAsync(auditDto);
        if (!res.Result)
        {
            return StatusCode(500, res);
        }
        return Ok(res);
    }
}
