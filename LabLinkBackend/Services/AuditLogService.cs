using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;

namespace LabLinkBackend.Services;

public class AuditLogService : LabLinkBackend.Services.IAuditLogService
{
    private readonly IAuditLogRepository _repository;

    public AuditLogService(IAuditLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<AuditLogResult> CreateLogAsync(AuditDto auditInfo)
    {
        var audit = new AuditLog
        {
            UserId = auditInfo.UserId,
            Action = auditInfo.Action,
            Resource = auditInfo.Resource,
            Timestamp = DateTime.UtcNow,
            Metadata = auditInfo.Metadata
        };
        return await _repository.AddAsync(audit);
    }
}
