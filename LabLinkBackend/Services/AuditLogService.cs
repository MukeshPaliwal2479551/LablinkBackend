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

    public async Task<AuditLogResult> CreateLogAsync(AuditDto dto)
    {
        var audit = new AuditLog
        {
            UserId = dto.UserId,
            Action = dto.Action,
            Resource = dto.Resource,
            Timestamp = DateTime.UtcNow,
            Metadata = dto.Metadata
        };
        return await _repository.AddAsync(audit);
    }
}
