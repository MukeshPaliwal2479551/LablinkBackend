
using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Repositories;

public class PanelRepository : IPanelRepository
{
    private readonly LabLinkDbContext _context;

    public PanelRepository(LabLinkDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsPanelCodeExistsAsync(string panelCode)
    {
        return await _context.Panels
            .AnyAsync(p => p.PanelCode == panelCode);
    }

    public async Task<List<Test>> GetActiveTestsByIdsAsync(List<int> testIds)
    {
        return await _context.Tests
            .Where(t => testIds.Contains(t.TestId) && t.IsActive)
            .ToListAsync();
    }


public async Task<Panel?> CreatePanelWithTestsAsync(PanelDto dto, List<Test> tests)
    {
        var panel = new Panel
        {
            PanelCode = dto.PanelCode!,
            PanelName = dto.PanelName!,
            IsActive = dto.IsActive ?? true
        };

        _context.Panels.Add(panel);
        await _context.SaveChangesAsync();

        foreach (var test in tests)
        {
            var panelTest = new PanelTest
            {
                PanelId = panel.PanelId,
                TestId = test.TestId,
                IsActive = true
            };
            _context.PanelTests.Add(panelTest);
        }

        await _context.SaveChangesAsync();
        return panel;
    }

    public async Task<Panel?> GetPanelByIdAsync(int panelId)
    {
        return await _context.Panels
            .Include(p => p.PanelTests)
            .FirstOrDefaultAsync(p => p.PanelId == panelId);
    }

    public async Task DeletePanelTestsByPanelIdAsync(int panelId)
    {
        await _context.PanelTests
            .Where(pt => pt.PanelId == panelId)
            .ExecuteDeleteAsync();
    }


    public async Task AddPanelTestsAsync(int panelId, List<Test> tests)
    {
        foreach (var test in tests)
        {
            _context.PanelTests.Add(new PanelTest
            {
                PanelId = panelId,
                TestId = test.TestId,
                IsActive = true
            });
        }
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePanelAsync(Panel panel)
    {
        _context.Update(panel);
        await _context.SaveChangesAsync();
    }

}
