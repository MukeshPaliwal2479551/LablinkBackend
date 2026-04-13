
using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;
using LabLinkBackend.Services;


namespace LabLinkBackend.Services;


public class PanelService : IPanelService
{
    private readonly IPanelRepository _repository;
    private readonly IAuditLogService _auditLogService;

    public PanelService(IPanelRepository repository, IAuditLogService auditLogService)
    {
        _repository = repository;
        _auditLogService = auditLogService;
    }


public async Task<(bool Success, PanelResultDto? Data, string? Error)> CreatePanelAsync(PanelDto dto)
    {
        // Validate panel code unique
        if (await _repository.IsPanelCodeExistsAsync(dto.PanelCode!))
        {
            return (false, null, "Panel code already exists");
        }

        // Get active tests
        var activeTests = await _repository.GetActiveTestsByIdsAsync(dto.TestIds);
        
        if (activeTests.Count != dto.TestIds.Count)
        {
            return (false, null, "One or more tests do not exist or are inactive. Only active tests allowed.");
        }

        // Create panel and tests
        var panel = await _repository.CreatePanelWithTestsAsync(dto, activeTests);
        if (panel == null)
        {
            return (false, null, "Failed to create panel");
        }

        // Map to result DTO
        var result = new PanelResultDto
        {
            PanelId = panel.PanelId,
            PanelCode = panel.PanelCode,
            PanelName = panel.PanelName,
            IsActive = panel.IsActive,
            CreatedAt = DateTime.UtcNow
        };


        return (true, result, null);
    }

    public async Task<(bool Success, PanelResultDto? Data, string? Error)> UpdatePanelAsync(PanelDto dto, int userId)
    {
        // Validate tests active
        var activeTests = await _repository.GetActiveTestsByIdsAsync(dto.TestIds);
        if (activeTests.Count != dto.TestIds.Count)
        {
            return (false, null, "One or more tests are inactive. Only active tests allowed.");
        }

        var panelId = dto.Id!.Value;
        var panel = await _repository.GetPanelByIdAsync(panelId);
        if (panel == null)
        {
            return (false, null, "Panel not found.");
        }

        if (!string.IsNullOrEmpty(dto.PanelName))
            panel.PanelName = dto.PanelName;
        if (dto.IsActive.HasValue)
            panel.IsActive = dto.IsActive.Value;

        await _repository.DeletePanelTestsByPanelIdAsync(panel.PanelId);
        await _repository.AddPanelTestsAsync(panel.PanelId, activeTests);
        await _repository.UpdatePanelAsync(panel);
  
        var auditDto = new AuditDto
        {
            UserId = userId,
            Action = "PanelUpdated",
            Resource = $"Panel/{panel.PanelId}",
            Metadata = $"Tests count: {dto.TestIds.Count}, Name: {panel.PanelName}, Active: {panel.IsActive}"
        };
        await _auditLogService.CreateLogAsync(auditDto);

        var result = new PanelResultDto
        {
            PanelId = panel.PanelId,
            PanelCode = panel.PanelCode,  // Unchanged
            PanelName = panel.PanelName,
            IsActive = panel.IsActive,
            CreatedAt = DateTime.UtcNow  // Reuse or add UpdatedAt
        };

        return (true, result, null);
    }

    public async Task<List<PanelResultDto>> GetAllPanelsAsync(string? panelName = null, string? panelCode = null)
    {
        var panels = await _repository.GetAllPanelsAsync(panelName, panelCode);
        return panels.Select(p => new PanelResultDto
        {
            PanelId = p.PanelId,
            PanelCode = p.PanelCode,
            PanelName = p.PanelName,
            IsActive = p.IsActive,
            CreatedAt = DateTime.MinValue  
        }).ToList();
    }

    public async Task<(bool Success, PanelResultDto? Data, string? Error)> DeactivatePanelAsync(int panelId, int userId)
    {
        if (await _repository.HasActiveOrderReferencesAsync(panelId))
        {
            return (false, null, "Cannot deactivate panel referenced by active orders.");
        }

        var panel = await _repository.GetPanelByIdAsync(panelId);
        if (panel == null)
        {
            return (false, null, "Panel not found.");
        }

        if (!panel.IsActive)
        {
            return (false, null, "Panel is already deactivated.");
        }

        panel.IsActive = false;
        await _repository.UpdatePanelAsync(panel);

       
        var auditDto = new AuditDto
        {
            UserId = userId,
            Action = "PanelDeactivated",
            Resource = $"Panel/{panel.PanelId}",
            Metadata = $"Panel: {panel.PanelName}"
        };
        await _auditLogService.CreateLogAsync(auditDto);

        var result = new PanelResultDto
        {
            PanelId = panel.PanelId,
            PanelCode = panel.PanelCode,
            PanelName = panel.PanelName,
            IsActive = false,
            CreatedAt = DateTime.MinValue  
        };

        return (true, result, null);
    }
}



