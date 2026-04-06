using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using  LabLinkBackend.Data;
namespace LabLinkBackend.Services;



public interface IUserService
{
    Task<bool> Delete(int id);
    Task<List<UserDto>> GetUsersAsync(string? name, string? phone);
}

