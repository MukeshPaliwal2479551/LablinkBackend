using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;

namespace LabLinkBackend.Services;


public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;

    public PatientService(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<PatientResponseDto> UpsertPatientAsync(PatientUpsertDto dto)
    {
        bool isCreate = dto.IsCreate;

        if (isCreate)
        {
            bool exists = await _repository.IsPatientExistAsync(
                dto.Name,
                dto.Dob,
                dto.ContactInfo
            );

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
        return MapToResponse(updated);
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
}