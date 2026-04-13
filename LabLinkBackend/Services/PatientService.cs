using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;
using Microsoft.AspNetCore.Http;

namespace LabLinkBackend.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;
    private readonly IAuditLogService _auditLogService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PatientService(
        IPatientRepository repository,
        IAuditLogService auditLogService,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _auditLogService = auditLogService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PatientResponseDto> UpsertPatientAsync(PatientUpsertDto dto)
    {
        try
        {
            if (dto.IsCreate)
            {
                bool exists = await _repository.IsPatientExistAsync(
                    dto.Name,
                    dto.Dob,
                    dto.ContactInfo);

                if (exists)
                    throw new InvalidOperationException("Duplicate patient detected.");

                var patient = new Patient
                {
                    UserId = dto.UserId,
                    Name = dto.Name,
                    Dob = dto.Dob,
                    Gender = dto.Gender,
                    ContactInfo = dto.ContactInfo,
                    Address = dto.Address,
                    IsActive = true,
                    PrimaryPhysicianName = dto.PrimaryPhysicianName,
                    CreatedOn = DateTime.UtcNow
                };

                var created = await _repository.AddAsync(patient);

                await _auditLogService.CreateLogAsync(new AuditDto
                {
                    UserId = GetCurrentUserId(),
                    Action = "CREATE",
                    Resource = "Patient",
                    Metadata =
                        $"PatientId={created.PatientId}, Name={created.Name}, Dob={created.Dob:yyyy-MM-dd}"
                });

                return MapToResponse(created);
            }

            var existing = await _repository.GetByIdAsync(dto.PatientId!.Value);
            if (existing == null)
                throw new InvalidOperationException("Patient not found.");

            existing.Name = dto.Name;
            existing.Dob = dto.Dob;
            existing.Gender = dto.Gender;
            existing.ContactInfo = dto.ContactInfo;
            existing.Address = dto.Address;
            existing.IsActive = dto.IsActive;
            existing.PrimaryPhysicianName = dto.PrimaryPhysicianName;

            var updated = await _repository.UpdateAsync(existing);

            await _auditLogService.CreateLogAsync(new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "UPDATE",
                Resource = "Patient",
                Metadata =
                    $"PatientId={updated.PatientId}, IsActive={updated.IsActive}"
            });

            return MapToResponse(updated);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                "An error occurred while creating or updating the patient.", ex);
        }
    }

    public async Task<PatientResponseDto?> GetByIdAsync(int patientId)
    {
        try
        {
            var patient = await _repository.GetByIdAsync(patientId);
            return patient == null ? null : MapToResponse(patient);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"Failed to retrieve patient (PatientId={patientId}).", ex);
        }
    }

    public async Task<List<PatientResponseDto>> GetAsync(string? name, string? phone)
    {
        try
        {
            var patients = await _repository.GetAsync(name, phone);
            return patients.Select(MapToResponse).ToList();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                "Failed to retrieve patients.", ex);
        }
    }

    public async Task DeleteAsync(int patientId)
    {
        try
        {
            var patient = await _repository.GetByIdAsync(patientId);
            if (patient == null)
                throw new InvalidOperationException("Patient not found.");

            await _repository.DeleteAsync(patient);

            await _auditLogService.CreateLogAsync(new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "DELETE",
                Resource = "Patient",
                Metadata = $"PatientId={patientId}"
            });
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"Failed to delete patient (PatientId={patientId}).", ex);
        }
    }


    private static PatientResponseDto MapToResponse(Patient patient)
    {
        return new PatientResponseDto
        {
            PatientId = patient.PatientId,
            UserId = patient.UserId,
            Name = patient.Name,
            Dob = patient.Dob,
            Gender = patient.Gender,
            ContactInfo = patient.ContactInfo,
            Address = patient.Address,
            IsActive = patient.IsActive,
            PrimaryPhysicianName = patient.PrimaryPhysicianName
        };
    }

    private int GetCurrentUserId()
    {
        var claimValue = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst("userId")?
            .Value;

        return int.TryParse(claimValue, out var userId) ? userId : 0;
    }
}