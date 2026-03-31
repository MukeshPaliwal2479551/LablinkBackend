using Microsoft.AspNetCore.Mvc;
using LabLinkBackend.DTO;
using LabLinkBackend.Services;

namespace LabLinkBackend.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuditController: ControllerBase
{
    private readonly IAuditLogService _auditLogService;

    public AuditController(IAuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    [HttpPost]
    [Route("CreateAudit")]
    public async Task<ActionResult<AuditLogResult>> CreateAudit([FromBody] AuditDto dto)
    {
        var res = await _auditLogService.CreateLogAsync(dto);
        if (!res.Result)
        {
            return StatusCode(500, res);
        }
        return Ok(res);
    }
}
