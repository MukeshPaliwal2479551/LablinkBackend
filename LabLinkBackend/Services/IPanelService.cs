using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IPanelService
{
    
    Task<(bool Success, PanelResultDto? Data, string? Error)> CreatePanelAsync(PanelDto panelInfo);
    Task<(bool Success, PanelResultDto? Data, string? Error)> UpdatePanelAsync(PanelDto updatePanelInfo, int userId);
    Task<List<PanelResultDto>> GetAllPanelsAsync(string? panelName = null, string? panelCode = null);
    Task<(bool Success, PanelResultDto? Data, string? Error)> DeactivatePanelAsync(int panelId, int userId);
}
