using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories;

public interface IAppointmentRepository
{
    Task<Appointment> AddAsync(Appointment appointment);
    Task<Appointment?> GetByIdAsync(int appointmentId);
    Task<List<Appointment>> GetByDateAsync(DateOnly? date);
    Task<Appointment> UpdateAsync(Appointment appointment);
    Task<bool> DeleteAsync(int appointmentId);
}
