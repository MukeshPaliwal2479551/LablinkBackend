using LabLinkBackend.DTO;
using LabLinkBackend.Repositories;

namespace LabLinkBackend.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClientListResult> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
}
