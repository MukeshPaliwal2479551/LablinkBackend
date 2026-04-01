using LabLinkBackend.Services;
using LabLinkBackend.Data;
using LabLinkBackend.Models;
using LabLinkBackend.DTO;

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
        
        var existingByEmail = await _userRepository.GetByEmail(userRegisterDTO.Email);
        if (existingByEmail != null)
            throw new InvalidOperationException("A user with this email already exists.");

        var existingByPhone = await _userRepository.GetByPhone(userRegisterDTO.Phone);
        if (existingByPhone != null)
            throw new InvalidOperationException("A user with this phone number already exists.");

        var user = new User
        {
            Name = userRegisterDTO.Name.Trim(),
            Email = string.IsNullOrWhiteSpace(userRegisterDTO.Email) ? 
                throw new InvalidOperationException("Invalid Email") : userRegisterDTO.Email.Trim(),
            Phone = string.IsNullOrWhiteSpace(userRegisterDTO.Phone) ?
                throw new InvalidOperationException("Invalid Phone number") : userRegisterDTO.Phone.Trim(),
            Password = userRegisterDTO.Password,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        };

        var createdUser = await _userRepository.CreateUser(user);

        // Create UserRole entries for each role
        foreach (var roleId in userRegisterDTO.RoleIds)
        {
            var userRole = new UserRole
            {
                UserId = createdUser.UserId,
                RoleId = roleId,
                AssignedAt = DateTime.UtcNow,
                IsActive = true
            };
            await _userRepository.CreateUserRole(userRole);
        }

        // Fetch the user with roles
        var userWithRoles = await _userRepository.GetByIdWithRoles(createdUser.UserId);
        return userWithRoles ?? createdUser;
    }

    public async Task<User> UpdateUser(int id, UserUpdateDTO userUpdateDTO)
    {

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
        existingUser.Email = string.IsNullOrWhiteSpace(userUpdateDTO.Email) ? 
            throw new InvalidOperationException("Invalid Email") : userUpdateDTO.Email.Trim();
        existingUser.Phone = string.IsNullOrWhiteSpace(userUpdateDTO.Phone) ?
            throw new InvalidOperationException("Invalid Phone number") : userUpdateDTO.Email.Trim();
        existingUser.IsActive = userUpdateDTO.IsActive;
        if (!string.IsNullOrWhiteSpace(userUpdateDTO.Password))
        {
            existingUser.Password = userUpdateDTO.Password;
        }
        existingUser.UpdatedOn = DateTime.UtcNow;

        return await _userRepository.UpdateUser(existingUser);
    }

    public async Task<User> UpdateUserRoles(int userId, List<int> roleIds)
    {
        var existingUser = await _userRepository.GetByIdWithRoles(userId);
        if (existingUser == null)
            throw new KeyNotFoundException("User not found.");

        await _userRepository.DeleteUserRolesByUserId(userId);

        if (roleIds != null && roleIds.Any())
        {
            var newUserRoles = roleIds.Distinct().Select(roleId => new UserRole
            {
                UserId = userId,
                RoleId = roleId,
                AssignedAt = DateTime.UtcNow,
                IsActive = true
            }).ToList();

            await _userRepository.AddUserRoles(newUserRoles);
        }

        var userWithUpdatedRoles = await _userRepository.GetByIdWithRoles(userId);
        return userWithUpdatedRoles ?? existingUser;
    }
}

