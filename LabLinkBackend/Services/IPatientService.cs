using LabLinkBackend.Models;

namespace LabLinkBackend.Services;

public interface IPatientService
{
    Task<Patient> UpsertPatientAsync(Patient patient);
}
