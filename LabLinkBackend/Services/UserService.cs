using LabLinkBackend.Services;
using LabLinkBackend.Data;
using LabLinkBackend.Models;
using LabLinkBackend.DTO;
using  LabLinkBackend.Data;
using System.Security.Cryptography.X509Certificates;
namespace LabLinkBackend.Services;


public class UserService : IUserService
{
    IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUser(UserRegisterDTO userRegisterDTO)
    {
        if (userRegisterDTO == null)
            throw new ArgumentNullException(nameof(userRegisterDTO));

        var existingByEmail = await _userRepository.GetByEmail(userRegisterDTO.Email);
        if (existingByEmail != null)
            throw new InvalidOperationException("A user with this email already exists.");


        var existingByPhone = await _userRepository.GetByPhone(userRegisterDTO.Phone);
        if (existingByPhone != null)
            throw new InvalidOperationException("A user with this phone number already exists.");


        var user = new User
        {
            Name = userRegisterDTO.Name.Trim(),
            Email = string.IsNullOrWhiteSpace(userRegisterDTO.Email) ? null : userRegisterDTO.Email.Trim(),
            Phone = string.IsNullOrWhiteSpace(userRegisterDTO.Phone) ? null : userRegisterDTO.Phone.Trim(),
            Password = userRegisterDTO.Password,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        };

        return await _userRepository.CreateUser(user);
    }

    public async Task<User> UpdateUser(int id, UserUpdateDTO userUpdateDTO)
    {
        if (userUpdateDTO == null)
            throw new ArgumentNullException(nameof(userUpdateDTO));

        var existingUser = await _userRepository.GetById(id);
        if (existingUser == null)
            throw new KeyNotFoundException("User not found.");

        var existingByEmail = await _userRepository.GetByEmail(userUpdateDTO.Email);
        if (existingByEmail != null && existingByEmail.UserId != id)
            throw new InvalidOperationException("A user with this email already exists.");

        var existingByPhone = await _userRepository.GetByPhone(userUpdateDTO.Phone);
        if (existingByPhone != null && existingByPhone.UserId != id)
            throw new InvalidOperationException("A user with this phone number already exists.");

        existingUser.Name = userUpdateDTO.Name.Trim();
        existingUser.Email = string.IsNullOrWhiteSpace(userUpdateDTO.Email) ? null : userUpdateDTO.Email.Trim();
        existingUser.Phone = string.IsNullOrWhiteSpace(userUpdateDTO.Phone) ? null : userUpdateDTO.Phone.Trim();
        existingUser.IsActive = userUpdateDTO.IsActive;
        if (!string.IsNullOrWhiteSpace(userUpdateDTO.Password))
        {
            existingUser.Password = userUpdateDTO.Password;
        }
        existingUser.UpdatedOn = DateTime.UtcNow;

        return await _userRepository.UpdateUser(existingUser);
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
        
    public async Task<bool> DeleteUser(int id)
    {
        return await _userRepository.DeleteUser(id);
    }

    public async Task<List<UserDto>> GetUsersAsync(string? name, string? phone)
    {
        var users = await _userRepository.GetUsersAsync(name, phone);
        return users.Select(MapToDto).ToList();
    }
}
 