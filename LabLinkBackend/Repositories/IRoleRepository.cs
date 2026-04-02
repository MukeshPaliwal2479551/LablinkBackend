using LabLinkBackend.Models;
namespace LabLinkBackend.Data;
public interface IRoleRepository
{
   
    Task<IEnumerable<Roles>> GetAllRolesAsync();
    Task<Roles?> GetRoleByIdAsync(int roleId);
}