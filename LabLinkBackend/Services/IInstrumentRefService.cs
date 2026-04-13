using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IInstrumentRefService
{
    Task<(bool Success, IEnumerable<InstrumentRefDto>? Data, string? Error)> GetAllAsync(string? name = null, string? section = null);
    Task<(bool Success, InstrumentRefDto? Data, string? Error)> GetByIdAsync(int instrumentId);
    Task<(bool Success, InstrumentRefDto? Data, string? Error)> UpsertAsync(InstrumentRefDto dto);
    Task<(bool Success, InstrumentRefDto? Data, string? Error)> UpdateDetailsAsync(int instrumentId, InstrumentRefDto dto);
    Task<(bool Success, string? Error)> DeleteAsync(int instrumentId);
}
