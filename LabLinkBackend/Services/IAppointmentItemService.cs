using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabLinkBackend.DTO;
using LabLinkBackend.Models;

namespace LabLinkBackend.Services
{
    public interface IAppointmentItemService
    {
        public Task<List<AppointmentItem>> CreateAsync(AppointmentItemDto appointmentItemDto);
        public Task<AppointmentItem> UpdateAsync(int AppItemId, AppointmentItemUpdateDto updateDto);
        public Task<AppointmentItem?> GetByIdAsync(int id);
        public Task<List<AppointmentItem>> GetByAppointmentIdAsync(int appointmentId);
        public Task DeleteAsync(int appointmentItemId);
    }
}

