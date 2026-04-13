using LabLinkBackend.Data;
using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Repositories;

public class InstrumentRefRepository : IInstrumentRefRepository
{
    private readonly LabLinkDbContext _context;

    public InstrumentRefRepository(LabLinkDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InstrumentRef>> GetAllAsync(string? name = null, string? section = null)
    {
        var query = _context.InstrumentRefs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(i => i.Name.Contains(name));

        if (!string.IsNullOrWhiteSpace(section))
            query = query.Where(i => i.Section != null && i.Section.Contains(section));

        return await query.ToListAsync();
    }

    public async Task<InstrumentRef?> GetByIdAsync(int instrumentId) =>
        await _context.InstrumentRefs.FindAsync(instrumentId);

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null) =>
        await _context.InstrumentRefs.AnyAsync(i =>
            i.Name.ToLower() == name.ToLower() &&
            (excludeId == null || i.InstrumentId != excludeId));

    public async Task<InstrumentRef> AddAsync(InstrumentRef instrument)
    {
        await _context.InstrumentRefs.AddAsync(instrument);
        await _context.SaveChangesAsync();
        return instrument;
    }

    public async Task<InstrumentRef> UpdateAsync(InstrumentRef instrument)
    {
        _context.InstrumentRefs.Update(instrument);
        await _context.SaveChangesAsync();
        return instrument;
    }

    public async Task<bool> DeleteAsync(int instrumentId)
    {
        var instrument = await _context.InstrumentRefs.FindAsync(instrumentId);
        if (instrument == null) return false;

        instrument.IsActive = false;
        _context.InstrumentRefs.Update(instrument);
        await _context.SaveChangesAsync();
        return true;
    }
}
