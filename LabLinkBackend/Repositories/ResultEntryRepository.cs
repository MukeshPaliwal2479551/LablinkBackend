using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Repositories;

public class ResultEntryRepository : IResultEntryRepository
{
    private readonly LabLinkDbContext _context;

    public ResultEntryRepository(LabLinkDbContext context)
    {
        _context = context;
    }

    public async Task<ResultEntry> AddAsync(ResultEntry resultEntry)
    {
        await _context.ResultEntries.AddAsync(resultEntry);
        await _context.SaveChangesAsync();
        return resultEntry;
    }

    public async Task<ResultEntry?> GetByIdAsync(int resultId) =>
        await _context.ResultEntries
            .Include(r => r.EnteredByNavigation)
            .Include(r => r.Flag)
            .Include(r => r.Test)
            .FirstOrDefaultAsync(r => r.ResultId == resultId && r.IsActive);

    public async Task<List<ResultEntry>> GetByOrderItemIdAsync(int orderItemId) =>
        await _context.ResultEntries
            .Include(r => r.EnteredByNavigation)
            .Include(r => r.Flag)
            .Include(r => r.Test)
            .Where(r => r.OrderItemId == orderItemId && r.IsActive)
            .ToListAsync();

    public async Task<ResultEntry> UpdateAsync(ResultEntry resultEntry)
    {
        _context.ResultEntries.Update(resultEntry);
        await _context.SaveChangesAsync();
        return resultEntry;
    }
}
