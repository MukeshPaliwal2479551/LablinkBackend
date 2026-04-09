
using LabLinkBackend.DTO;
using LabLinkBackend.Models;

namespace LabLinkBackend.Services;

public interface IUserService
{
    public Task<object> CreateUser(UserRegisterDTO user);
    public Task<object> UpdateUserAndRoles(int id, UserUpdateDTO user);
}
