using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Data;

namespace LabLinkBackend.Services;

public interface IUserService
{
    public Task<object> CreateUser(UserRegisterDTO user);
    public Task<object> UpdateUserAndRoles(int id, UserUpdateDTO user);
    Task<bool> Delete(int id);
    Task<List<UserDto>> GetUsersAsync(string name, string phone);
}
