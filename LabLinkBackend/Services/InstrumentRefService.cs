using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Services;

public class InstrumentRefService : IInstrumentRefService
{
    private readonly IInstrumentRefRepository _repository;
    private readonly IAuditLogService _auditLogService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public InstrumentRefService(
        IInstrumentRefRepository repository,
        IAuditLogService auditLogService,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _auditLogService = auditLogService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<(bool Success, IEnumerable<InstrumentRefDto>? Data, string? Error)> GetAllAsync(string? name = null, string? section = null)
    {
        try
        {
            var instruments = await _repository.GetAllAsync(name, section);
            return (true, instruments.Select(MapToDto), null);
        }
        catch (DbUpdateException ex)
        {
            return (false, null, $"Database error while fetching instruments: {ex.InnerException?.Message ?? ex.Message}");
        }
        catch (Exception ex)
        {
            return (false, null, $"Unexpected error while fetching instruments: {ex.Message}");
        }
    }

    public async Task<(bool Success, InstrumentRefDto? Data, string? Error)> GetByIdAsync(int instrumentId)
    {
        try
        {
            var instrument = await _repository.GetByIdAsync(instrumentId);
            if (instrument == null)
                return (false, null, $"Instrument with ID {instrumentId} not found.");

            return (true, MapToDto(instrument), null);
        }
        catch (DbUpdateException ex)
        {
            return (false, null, $"Database error while fetching instrument: {ex.InnerException?.Message ?? ex.Message}");
        }
        catch (Exception ex)
        {
            return (false, null, $"Unexpected error while fetching instrument: {ex.Message}");
        }
    }

    public async Task<(bool Success, InstrumentRefDto? Data, string? Error)> UpsertAsync(InstrumentRefDto dto)
    {
        try
        {
            if (dto.InstrumentId == 0)
            {
                bool nameExists = await _repository.ExistsByNameAsync(dto.Name);
                if (nameExists)
                    return (false, null, "An instrument with this name already exists.");

                var instrument = new InstrumentRef
                {
                    Name = dto.Name,
                    Model = dto.Model,
                    Section = dto.Section,
                    InterfaceTypeId = dto.InterfaceTypeId,
                    IsActive = true
                };

                var created = await _repository.AddAsync(instrument);

                await _auditLogService.CreateLogAsync(new AuditDto
                {
                    UserId = GetCurrentUserId(),
                    Action = "InstrumentCreated",
                    Resource = $"InstrumentRef/{created.InstrumentId}",
                    Metadata = $"Name={created.Name}, Model={created.Model}, Section={created.Section}"
                });

                return (true, MapToDto(created), null);
            }

            var existing = await _repository.GetByIdAsync(dto.InstrumentId);
            if (existing == null)
                return (false, null, $"Instrument with ID {dto.InstrumentId} not found.");

            bool duplicateName = await _repository.ExistsByNameAsync(dto.Name, dto.InstrumentId);
            if (duplicateName)
                return (false, null, "An instrument with this name already exists.");

            existing.Name = dto.Name;
            existing.Model = dto.Model;
            existing.Section = dto.Section;
            existing.InterfaceTypeId = dto.InterfaceTypeId;
            existing.IsActive = dto.IsActive;

            var updated = await _repository.UpdateAsync(existing);

            await _auditLogService.CreateLogAsync(new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "InstrumentUpdated",
                Resource = $"InstrumentRef/{updated.InstrumentId}",
                Metadata = $"Name={updated.Name}, Model={updated.Model}, Section={updated.Section}, Active={updated.IsActive}"
            });

            return (true, MapToDto(updated), null);
        }
        catch (DbUpdateException ex)
        {
            return (false, null, $"Database error while saving instrument: {ex.InnerException?.Message ?? ex.Message}");
        }
        catch (Exception ex)
        {
            return (false, null, $"Unexpected error while saving instrument: {ex.Message}");
        }
    }

    public async Task<(bool Success, InstrumentRefDto? Data, string? Error)> UpdateDetailsAsync(int instrumentId, InstrumentRefDto dto)
    {
        try
        {
            var existing = await _repository.GetByIdAsync(instrumentId);
            if (existing == null)
                return (false, null, $"Instrument with ID {instrumentId} not found.");

            bool duplicateName = await _repository.ExistsByNameAsync(dto.Name, instrumentId);
            if (duplicateName)
                return (false, null, "An instrument with this name already exists.");

            existing.Name = dto.Name;
            existing.Model = dto.Model;
            existing.Section = dto.Section;

            var updated = await _repository.UpdateAsync(existing);

            await _auditLogService.CreateLogAsync(new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "InstrumentDetailsUpdated",
                Resource = $"InstrumentRef/{updated.InstrumentId}",
                Metadata = $"Name={updated.Name}, Model={updated.Model}, Section={updated.Section}"
            });

            return (true, MapToDto(updated), null);
        }
        catch (DbUpdateException ex)
        {
            return (false, null, $"Database error while updating instrument: {ex.InnerException?.Message ?? ex.Message}");
        }
        catch (Exception ex)
        {
            return (false, null, $"Unexpected error while updating instrument: {ex.Message}");
        }
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(int instrumentId)
    {
        try
        {
            bool deleted = await _repository.DeleteAsync(instrumentId);
            if (!deleted)
                return (false, $"Instrument with ID {instrumentId} not found.");

            await _auditLogService.CreateLogAsync(new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "InstrumentDeleted",
                Resource = $"InstrumentRef/{instrumentId}",
                Metadata = $"InstrumentId={instrumentId}"
            });

            return (true, null);
        }
        catch (DbUpdateException ex)
        {
            return (false, $"Database error while deleting instrument: {ex.InnerException?.Message ?? ex.Message}");
        }
        catch (Exception ex)
        {
            return (false, $"Unexpected error while deleting instrument: {ex.Message}");
        }
    }

    private static InstrumentRefDto MapToDto(InstrumentRef instrument) => new()
    {
        InstrumentId = instrument.InstrumentId,
        Name = instrument.Name,
        Model = instrument.Model,
        Section = instrument.Section,
        InterfaceTypeId = instrument.InterfaceTypeId,
        IsActive = instrument.IsActive
    };

    private int GetCurrentUserId()
    {
        var claimValue = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;
        return int.TryParse(claimValue, out var userId) ? userId : 0;
    }
}
