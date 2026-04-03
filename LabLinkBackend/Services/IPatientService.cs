using LabLinkBackend.Models;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IPatientService
{
    Task<PatientResponseDto> UpsertPatientAsync(PatientUpsertDto dto);
}
