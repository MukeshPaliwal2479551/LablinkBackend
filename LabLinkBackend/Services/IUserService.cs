using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using  LabLinkBackend.Data;
namespace LabLinkBackend.Services;



public interface IUserService
{
    public Task<User> CreateUser(UserRegisterDTO user);
    public Task<User> UpdateUser(int id, UserUpdateDTO user);
    Task<bool> Delete(int id);
    Task<List<UserDto>> GetUsersAsync(string? name, string? phone);
}

