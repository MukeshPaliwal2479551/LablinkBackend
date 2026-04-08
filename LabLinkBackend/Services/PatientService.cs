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

    public async Task<PatientResponseDto> UpsertPatientAsync(PatientUpsertDto patientUpsertDto)
    {
        bool isCreate = patientUpsertDto.IsCreate;

        if (isCreate)
        {
            bool exists = await _repository.IsPatientExistAsync(
                patientUpsertDto.Name,
                patientUpsertDto.Dob,
                patientUpsertDto.ContactInfo
            );

            if (exists)
                throw new InvalidOperationException("Duplicate patient detected.");

            var patient = new Patient
            {
                UserId = patientUpsertDto.UserId,
                Name = patientUpsertDto.Name,
                Dob = patientUpsertDto.Dob,
                Gender = patientUpsertDto.Gender,
                ContactInfo = patientUpsertDto.ContactInfo,
                Address = patientUpsertDto.Address,
                IsActive = true,
                PrimaryPhysicianName = patientUpsertDto.PrimaryPhysicianName,
                CreatedOn = DateTime.UtcNow
            };

            var created = await _repository.AddAsync(patient);

            return MapToResponse(created);
        }

        var existing = await _repository.GetByIdAsync(patientUpsertDto.PatientId!.Value);

        if (existing == null)
            throw new InvalidOperationException("Patient not found.");

        existing.Name = patientUpsertDto.Name;
        existing.Dob = patientUpsertDto.Dob;
        existing.Gender = patientUpsertDto.Gender;
        existing.ContactInfo = patientUpsertDto.ContactInfo;
        existing.Address = patientUpsertDto.Address;
        existing.IsActive = patientUpsertDto.IsActive;
        existing.PrimaryPhysicianName = patientUpsertDto.PrimaryPhysicianName;

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