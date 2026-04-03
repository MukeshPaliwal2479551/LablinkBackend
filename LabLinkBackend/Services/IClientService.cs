using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IClientService
{
    Task<ClientListResult> GetAllAsync();
}
