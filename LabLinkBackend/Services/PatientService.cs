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

    public async Task<Patient> UpsertPatientAsync(Patient patient)
    {

        if (patient.UserId <= 0)
            throw new InvalidOperationException("Authenticated user is required.");

        if (string.IsNullOrWhiteSpace(patient.Name))
            throw new InvalidOperationException("Patient name is required.");

        if (patient.Dob == null)
            throw new InvalidOperationException("Date of birth is required.");

        if (string.IsNullOrWhiteSpace(patient.Gender))
            throw new InvalidOperationException("Gender is required.");

        if (string.IsNullOrWhiteSpace(patient.ContactInfo))
            throw new InvalidOperationException("Contact information is required.");


        var duplicate = await _repository
            .GetByNameDobPhoneAsync(patient.Name, patient.Dob, patient.ContactInfo);

        if (duplicate != null && patient.PatientId == 0)
            throw new InvalidOperationException("Duplicate patient detected.");

        var existing = await _repository.GetByIdAsync(patient.PatientId);


        if (existing == null)
        {
            patient.IsActive = true;
            patient.CreatedOn = DateTime.UtcNow;

            var newPatient = await _repository.AddAsync(patient);
            await _repository.SaveChangesAsync();

            return newPatient;
        }




        if (existing.UserId != patient.UserId)
            throw new UnauthorizedAccessException("You cannot modify another user's patient record.");

        existing.Name = patient.Name;
        existing.Dob = patient.Dob;
        existing.Gender = patient.Gender;
        existing.ContactInfo = patient.ContactInfo;
        existing.Address = patient.Address;
        existing.IsActive = patient.IsActive;
        existing.PrimaryPhysicianName = patient.PrimaryPhysicianName;

        var updatedPatient = await _repository.UpdateAsync(existing);
        await _repository.SaveChangesAsync();

        return updatedPatient;
    }
}