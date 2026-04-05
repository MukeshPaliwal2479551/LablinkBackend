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
        var res = await _auditLogService.CreateLogAsync(AuditInfo);
        return Ok(res);
    }
}
