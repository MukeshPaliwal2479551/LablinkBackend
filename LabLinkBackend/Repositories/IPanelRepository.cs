
using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Repositories;

public interface IPanelRepository
{
    Task<bool> IsPanelCodeExistsAsync(string panelCode);
    Task<List<Test>> GetActiveTestsByIdsAsync(List<int> testIds);

Task<Panel?> CreatePanelWithTestsAsync(PanelDto dto, List<Test> tests);
    
    // Update methods
    Task<Panel?> GetPanelByIdAsync(int panelId);
    Task DeletePanelTestsByPanelIdAsync(int panelId);

    Task AddPanelTestsAsync(int panelId, List<Test> tests);
    Task UpdatePanelAsync(Panel panel);

    Task<List<Panel>> GetAllPanelsAsync();
    Task<bool> HasActiveOrderReferencesAsync(int panelId);
}
