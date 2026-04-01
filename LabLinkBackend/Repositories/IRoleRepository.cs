using LabLinkBackend.Models;
namespace LabLinkBackend.Data;
public interface IRoleRepository
{
    Task<Roles> CreateRoleAsync(Roles role);
    Task<IEnumerable<Roles>> GetAllRolesAsync();
    Task<Roles?> GetRoleByIdAsync(int roleId);
}