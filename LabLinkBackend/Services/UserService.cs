using LabLinkBackend.Services;
using LabLinkBackend.Data;
using LabLinkBackend.Models;
using LabLinkBackend.DTO;
using  LabLinkBackend.Data;
using System.Security.Cryptography.X509Certificates;
using LabLinkBackend.Repositories;
namespace LabLinkBackend.Services;


public class UserService : IUserService
{
    IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    } 
    public async Task<bool> Delete(int id)
    {
        return await _userRepository.Delete(id);
    }


    public async Task<List<UserDto>> GetUsersAsync(string? name, string? phone)
    {
        var users = await _userRepository.GetUsersAsync(name, phone);
        return users.Select(MapToDto).ToList();
    }

    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            IsActive = user.IsActive
        };
    }
 
}
 