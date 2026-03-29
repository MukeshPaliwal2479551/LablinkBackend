using LabLinkBackend.DTO;
using LabLinkBackend.Models;

namespace LabLinkBackend.Services;

public interface IUserService
{
    public Task<User> CreateUser(UserRegisterDTO user);
    public Task<User> UpdateUser(int id, UserUpdateDTO user);
}

