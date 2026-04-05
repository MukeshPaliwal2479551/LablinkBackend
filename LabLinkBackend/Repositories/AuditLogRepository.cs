using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Repositories;

public class AuditLogRepository : LabLinkBackend.Repositories.IAuditLogRepository
{
    private readonly LabLinkDbContext _context;

    public AuditLogRepository(LabLinkDbContext context)
    {
        _context = context;
    }

    public async Task<AuditLogResult> AddAsync(AuditLog log)
    {
        var res = new AuditLogResult();
        try
        {
            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
            res.Result = true;
            res.Message = null;
        }
        catch (Exception ex)
        {
            res.Result = false;
            res.Message = $"Unexpected error: {ex.Message}";
        }
        return res;
    }
}
