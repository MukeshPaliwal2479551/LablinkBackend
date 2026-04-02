using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IPanelService
{
    
    Task<(bool Success, PanelResultDto? Data, string? Error)> CreatePanelAsync(CreatePanelDto dto);
    Task<(bool Success, PanelResultDto? Data, string? Error)> UpdatePanelAsync(UpdatePanelDto dto, int userId);

}
