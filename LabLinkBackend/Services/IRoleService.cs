using Role.DTO;
using  LabLinkBackend.Data;
using LabLinkBackend.Models;
namespace LabLinkBackend.Services;
public interface IRoleService
{
    Task<Roles> CreateRoleAsync(RoleDto dto);
    Task<IEnumerable<Roles>> GetAllRolesAsync();
}
