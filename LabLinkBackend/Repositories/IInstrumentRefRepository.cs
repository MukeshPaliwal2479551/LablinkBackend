using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories;

public interface IInstrumentRefRepository
{
    Task<IEnumerable<InstrumentRef>> GetAllAsync(string? name = null, string? section = null);
    Task<InstrumentRef?> GetByIdAsync(int instrumentId);
    Task<bool> ExistsByNameAsync(string name, int? excludeId = null);
    Task<InstrumentRef> AddAsync(InstrumentRef instrument);
    Task<InstrumentRef> UpdateAsync(InstrumentRef instrument);
    Task<bool> DeleteAsync(int instrumentId);
}
