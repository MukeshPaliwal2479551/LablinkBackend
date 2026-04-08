using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IAppointmentService
{
    Task<AppointmentResponseDto> CreateAsync(AppointmentCreateDto dto);
    Task<AppointmentResponseDto> UpdateAsync(int appointmentId, AppointmentUpdateDto dto);
    Task<bool> DeleteAsync(int appointmentId);
    Task<AppointmentResponseDto> GetByIdAsync(int appointmentId);
    Task<List<AppointmentResponseDto>> GetByDateAsync(DateOnly? date);
}
