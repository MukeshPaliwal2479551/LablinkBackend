using LabLinkBackend.Models;
using Role.DTO;
using  LabLinkBackend.Data;
namespace LabLinkBackend.Services;
public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    

    public async Task<IEnumerable<Roles>> GetAllRolesAsync()
    {
        return await _repository.GetAllRolesAsync();
    }
}