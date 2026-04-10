using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories
{
    public interface IAppointmentItemRepository
    {
        Task<AppointmentItem> CreateAsync(AppointmentItem appointmentItem);
        Task<AppointmentItem> UpdateAsync(AppointmentItem appointmentItem);
        Task<AppointmentItem?> GetByIdAsync(int id);
        Task<List<AppointmentItem>> GetByAppointmentIdAsync(int appointmentId);
        Task DeleteAsync(int id);
        Task<List<int>> GetTestIdsByPanelIdAsync(int panelId);
    }
}