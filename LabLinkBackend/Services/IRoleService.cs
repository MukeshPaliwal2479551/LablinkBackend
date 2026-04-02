using Role.DTO;
using  LabLinkBackend.Data;
using LabLinkBackend.Models;
namespace LabLinkBackend.Services;
public interface IRoleService
{
  
    Task<IEnumerable<Roles>> GetAllRolesAsync();
}
