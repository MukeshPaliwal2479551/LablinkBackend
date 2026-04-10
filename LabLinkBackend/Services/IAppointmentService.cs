using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IAppointmentService
{
    Task<AppointmentDto> CreateAsync(AppointmentDto dto);
    Task<AppointmentDto> UpdateAsync(int appointmentId, AppointmentDto dto);
    Task<bool> DeleteAsync(int appointmentId);
    Task<AppointmentDto> GetByIdAsync(int appointmentId);
    Task<List<AppointmentDto>> GetByDateAsync(DateOnly? date);
}
