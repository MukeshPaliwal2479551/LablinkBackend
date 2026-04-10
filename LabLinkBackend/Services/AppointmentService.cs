using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;
using Microsoft.AspNetCore.Http;

namespace LabLinkBackend.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;
    private readonly IAuditLogService _auditLogService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppointmentService(
        IAppointmentRepository repository,
        IAuditLogService auditLogService,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _auditLogService = auditLogService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AppointmentDto> CreateAsync(AppointmentDto dto)
    {
        var appointment = new Appointment
        {
            PatientId = dto.PatientId,
            BookedDateTime = dto.BookedDateTime,
            Address = dto.Address,
            VisitTypeId = dto.VisitTypeId,
            PhlebotomistId = dto.PhlebotomistId,
            IsActive = true
        };

        var created = await _repository.AddAsync(appointment);

        await _auditLogService.CreateLogAsync(new AuditDto
        {
            UserId = GetCurrentUserId(),
            Action = "AppointmentCreated",
            Resource = $"Appointment/{created.AppointmentId}",
            Metadata = $"PatientId={created.PatientId}, Date={created.BookedDateTime:o}, Address={created.Address}"
        });

        return MapToDto(created);
    }

    public async Task<AppointmentDto> UpdateAsync(int appointmentId, AppointmentDto dto)
    {
        var appointment = await _repository.GetByIdAsync(appointmentId)
            ?? throw new InvalidOperationException("Appointment not found.");

        appointment.BookedDateTime = dto.BookedDateTime;
        appointment.Address = dto.Address;

        var updated = await _repository.UpdateAsync(appointment);

        await _auditLogService.CreateLogAsync(new AuditDto
        {
            UserId = GetCurrentUserId(),
            Action = "AppointmentUpdated",
            Resource = $"Appointment/{updated.AppointmentId}",
            Metadata = $"PatientId={updated.PatientId}, Date={updated.BookedDateTime:o}, Address={updated.Address}"
        });

        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int appointmentId)
    {
        bool deleted = await _repository.DeleteAsync(appointmentId);
        if (!deleted)
            throw new InvalidOperationException("Appointment not found.");

        await _auditLogService.CreateLogAsync(new AuditDto
        {
            UserId = GetCurrentUserId(),
            Action = "AppointmentDeleted",
            Resource = $"Appointment/{appointmentId}",
            Metadata = $"AppointmentId={appointmentId}"
        });

        return true;
    }

    public async Task<AppointmentDto> GetByIdAsync(int appointmentId)
    {
        var appointment = await _repository.GetByIdAsync(appointmentId)
            ?? throw new InvalidOperationException("Appointment not found.");
        return MapToDto(appointment);
    }

    public async Task<List<AppointmentDto>> GetByDateAsync(DateOnly? date)
    {
        var appointments = await _repository.GetByDateAsync(date);
        return appointments.Select(MapToDto).ToList();
    }

    private static AppointmentDto MapToDto(Appointment a) => new()
    {
        AppointmentId = a.AppointmentId,
        PatientId = a.PatientId,
        BookedDateTime = a.BookedDateTime,
        Address = a.Address,
        VisitTypeId = a.VisitTypeId,
        PhlebotomistId = a.PhlebotomistId,
        IsActive = a.IsActive
    };

    private int GetCurrentUserId()
    {
        var claimValue = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;
        return int.TryParse(claimValue, out var userId) ? userId : 0;
    }
}
