using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LabLinkBackend.Repositories
{
    public class AppointmentItemRepository : IAppointmentItemRepository
    {
        private readonly LabLinkDbContext _labLinkDbContext;
        public AppointmentItemRepository(LabLinkDbContext labLinkDbContext)
        {
            _labLinkDbContext = labLinkDbContext;
        }

        public async Task<AppointmentItem> CreateAsync(AppointmentItem appointmentItem)
        {
            _labLinkDbContext.AppointmentItems.Add(appointmentItem);
            await _labLinkDbContext.SaveChangesAsync();
            return appointmentItem;
        }

        public async Task<AppointmentItem> UpdateAsync(AppointmentItem appointmentItem)
        {
            _labLinkDbContext.AppointmentItems.Update(appointmentItem);
            await _labLinkDbContext.SaveChangesAsync();
            return appointmentItem;
        }

        public async Task<AppointmentItem?> GetByIdAsync(int id)
        {
            return await _labLinkDbContext.AppointmentItems.FindAsync(id);
        }

        public async Task<List<AppointmentItem>> GetByAppointmentIdAsync(int appointmentId)
        {
            return await _labLinkDbContext.AppointmentItems
                .Where(ai => ai.AppointmentId == appointmentId)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var appointmentItem = await _labLinkDbContext.AppointmentItems.FindAsync(id);
            if (appointmentItem != null)
            {
                _labLinkDbContext.AppointmentItems.Remove(appointmentItem);
                await _labLinkDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<int>> GetTestIdsByPanelIdAsync(int panelId)
        {
            return await _labLinkDbContext.PanelTests
                .Where(pt => pt.PanelId == panelId && pt.IsActive)
                .Select(pt => pt.TestId)
                .ToListAsync();
        }
    }
}