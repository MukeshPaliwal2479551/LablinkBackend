using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(int patientId);

    Task<bool> IsPatientExistAsync(string name,DateOnly dob,string phone);

    Task<Patient> AddAsync(Patient patient);

    Task<Patient> UpdateAsync(Patient patient);

}