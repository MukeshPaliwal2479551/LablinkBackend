using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using  LabLinkBackend.Data;
namespace LabLinkBackend.Services;



public interface IUserService
{
    public Task<User> CreateUser(UserRegisterDTO user);
    public Task<User> UpdateUser(int id, UserUpdateDTO user);
}

