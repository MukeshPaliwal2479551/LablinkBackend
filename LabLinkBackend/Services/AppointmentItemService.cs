using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;
// using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Services
{
    public class AppointmentItemService : IAppointmentItemService
    {
        private readonly IAppointmentItemRepository _appointmentItemRepository;
        public AppointmentItemService(IAppointmentItemRepository appointmentItemRepository)
        {
            _appointmentItemRepository = appointmentItemRepository;
        }
        public async Task<List<AppointmentItem>> CreateAsync(AppointmentItemDto appointmentItemDto)
        {
            var items = new List<AppointmentItem>();
            
            if (appointmentItemDto.PanelId.HasValue)
            {
                // Fetch test IDs for the panel
                var testIds = await _appointmentItemRepository.GetTestIdsByPanelIdAsync(appointmentItemDto.PanelId.Value);
                
                foreach (var testId in testIds)
                {
                    var appItem = new AppointmentItem
                    {
                        AppointmentId = appointmentItemDto.AppointmentId,
                        TestId = testId,
                        PanelId = appointmentItemDto.PanelId,
                        Instructions = appointmentItemDto.Instructions,
                        Priority = appointmentItemDto.Priority,
                        IsActive = appointmentItemDto.IsActive ?? true
                    };
                    items.Add(appItem);
                }
            }

            if (appointmentItemDto.TestId.HasValue)
            {
                // Create single item with TestId and PanelId as 1
                var appItem = new AppointmentItem
                {
                    AppointmentId = appointmentItemDto.AppointmentId,
                    TestId = appointmentItemDto.TestId,
                    PanelId = 1, // As per user request
                    Instructions = appointmentItemDto.Instructions,
                    Priority = appointmentItemDto.Priority,
                    IsActive = appointmentItemDto.IsActive ?? true
                };
                items.Add(appItem);
            }
            
            foreach (var item in items)
            {
                await _appointmentItemRepository.CreateAsync(item);
            }
            return items;
        }

        public async Task<AppointmentItem> UpdateAsync(int AppItemId, AppointmentItemUpdateDto updateDto)
        {
            var existingItem = await _appointmentItemRepository.GetByIdAsync(AppItemId);
            if (existingItem == null)
            {
                throw new KeyNotFoundException($"Appointment item with ID {AppItemId} not found.");
            }

            existingItem.Priority = updateDto.Priority;
            existingItem.Instructions = updateDto.Instructions;
            existingItem.IsActive = updateDto.IsActive ?? existingItem.IsActive;

            return await _appointmentItemRepository.UpdateAsync(existingItem);
        }

        public async Task<AppointmentItem?> GetByIdAsync(int id)
        {
            return await _appointmentItemRepository.GetByIdAsync(id);
        }

        public async Task<List<AppointmentItem>> GetByAppointmentIdAsync(int appointmentId)
        {
            return await _appointmentItemRepository.GetByAppointmentIdAsync(appointmentId);
        }

        public async Task DeleteAsync(int appointmentItemId)
        {
            var existingItem = await _appointmentItemRepository.GetByIdAsync(appointmentItemId);
            if (existingItem == null)
            {
                throw new KeyNotFoundException($"Appointment item with ID {appointmentItemId} not found.");
            }

            await _appointmentItemRepository.DeleteAsync(appointmentItemId);
        }
    }
}
 