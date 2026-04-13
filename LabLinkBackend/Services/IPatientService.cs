using LabLinkBackend.Models;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IPatientService
{
    Task<PatientResponseDto> UpsertPatientAsync(PatientUpsertDto dto);

    Task<PatientResponseDto?> GetByIdAsync(int patientId);
    Task<List<PatientResponseDto>> GetAsync(string? name, string? phone);

    Task DeleteAsync(int patientId);

}
