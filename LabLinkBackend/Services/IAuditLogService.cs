using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IAuditLogService
{
    Task<AuditLogResult> CreateLogAsync(AuditDto dto);
}
