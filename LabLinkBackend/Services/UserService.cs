using LabLinkBackend.Services;
using LabLinkBackend.Data;
using LabLinkBackend.Models;
using LabLinkBackend.DTO;
using LabLinkBackend.Repositories;

namespace LabLinkBackend.Services;

public class UserService : IUserService
{
    IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<object> CreateUser(UserRegisterDTO userRegisterDTO)
    {
        var existingByEmail = await _userRepository.GetByEmail(userRegisterDTO.Email);
        if (existingByEmail != null)
            throw new InvalidOperationException("A user with this email already exists.");

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
        var userResult = userWithRoles ?? createdUser;
        return new {
            message = "user created",
            data = new {
                userResult.UserId,
                userResult.Name,
                userResult.Email,
                userResult.Phone,
                userResult.IsActive,
                userResult.CreatedOn,
                Roles = userResult.UserRoles?.Select(ur => new {
                    ur.RoleId,
                    RoleName = ur.Roles?.Role
                }).ToList()
            }
        };
    }

    public async Task<object> UpdateUserAndRoles(int id, UserUpdateDTO userUpdateDTO)
    {
        var existingUser = await _userRepository.GetById(id);
        if (existingUser == null)
            throw new KeyNotFoundException("User not found.");

        existingUser.Name = userUpdateDTO.Name.Trim();
        existingUser.Phone = string.IsNullOrWhiteSpace(userUpdateDTO.Phone) ?
            throw new InvalidOperationException("Invalid Phone number") : userUpdateDTO.Phone.Trim();
        existingUser.IsActive = userUpdateDTO.IsActive;
        if (!string.IsNullOrWhiteSpace(userUpdateDTO.Password))
        {
            existingUser.Password = userUpdateDTO.Password;
        }
        existingUser.UpdatedOn = DateTime.UtcNow;

        await _userRepository.UpdateUser(existingUser);

        // Update roles
        await _userRepository.DeleteUserRolesByUserId(id);
        if (userUpdateDTO.RoleIds != null && userUpdateDTO.RoleIds.Any())
        {
            var newUserRoles = userUpdateDTO.RoleIds.Distinct().Select(roleId => new UserRole
            {
                UserId = id,
                RoleId = roleId,
                AssignedAt = DateTime.UtcNow,
                IsActive = true
            }).ToList();
            await _userRepository.AddUserRoles(newUserRoles);
        }

        var userWithUpdatedRoles = await _userRepository.GetByIdWithRoles(id);
        var user = userWithUpdatedRoles ?? existingUser;
        return new {
            message = "User and roles updated successfully",
            data = new {
                user.UserId,
                user.Name,
                user.Phone,
                user.IsActive,
                user.UpdatedOn,
                user.Email,
                Roles = user.UserRoles?.Select(ur => new {
                    ur.RoleId,
                    RoleName = ur.Roles?.Role
                }).ToList()
            }
        };
    }
}

