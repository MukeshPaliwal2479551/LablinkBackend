using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly LabLinkDbContext _context;

    public AppointmentRepository(LabLinkDbContext context)
    {
        _context = context;
    }

    public async Task<Appointment> AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment?> GetByIdAsync(int appointmentId) =>
        await _context.Appointments.FindAsync(appointmentId);

    public async Task<List<Appointment>> GetByDateAsync(DateOnly? date)
    {
        var query = _context.Appointments.Where(a => a.IsActive);

        if (date.HasValue)
            query = query.Where(a => DateOnly.FromDateTime(a.BookedDateTime) == date.Value);

        return await query.ToListAsync();
    }

    public async Task<Appointment> UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<bool> DeleteAsync(int appointmentId)
    {
        var appointment = await _context.Appointments.FindAsync(appointmentId);
        if (appointment == null) return false;

        appointment.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}
