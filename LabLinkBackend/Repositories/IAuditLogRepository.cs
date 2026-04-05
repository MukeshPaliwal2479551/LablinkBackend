using LabLinkBackend.DTO;
using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories;

public interface IAuditLogRepository
{
    Task<AuditLogResult> AddAsync(AuditLog log);
}
