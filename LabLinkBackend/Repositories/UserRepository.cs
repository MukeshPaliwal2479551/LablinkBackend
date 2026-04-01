using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using LabLinkBackend.Data;
using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Data;

public class UserRepository : IUserRepository
{
    LabLinkDbContext _labLinkDbContext;
    public UserRepository(LabLinkDbContext labLinkDbContext)
    {
        _labLinkDbContext = labLinkDbContext;
    }
    async public Task<User> CreateUser(User user)
    {
        await _labLinkDbContext.Users.AddAsync(user);
        await _labLinkDbContext.SaveChangesAsync();
        return user;
    }

    async public Task<User?> GetByEmail(string email)
    {
        return await _labLinkDbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    async public Task<User?> GetByPhone(string phone)
    {
        return await _labLinkDbContext.Users
            .FirstOrDefaultAsync(u => u.Phone == phone);
    }

    async public Task<User?> GetById(int id)
    {
        return await _labLinkDbContext.Users
            .FirstOrDefaultAsync(u => u.UserId == id);
    }

    async public Task<User> UpdateUser(User user)
    {
        _labLinkDbContext.Users.Update(user);
        await _labLinkDbContext.SaveChangesAsync();
        return user;
    }

    async public Task CreateUserRole(UserRole userRole)
    {
        await _labLinkDbContext.UserRoles.AddAsync(userRole);
        await _labLinkDbContext.SaveChangesAsync();
    }

    async public Task DeleteUserRolesByUserId(int userId)
    {
        var existingRoles = _labLinkDbContext.UserRoles.Where(ur => ur.UserId == userId);
        _labLinkDbContext.UserRoles.RemoveRange(existingRoles);
        await _labLinkDbContext.SaveChangesAsync();
    }

    async public Task AddUserRoles(IEnumerable<UserRole> userRoles)
    {
        await _labLinkDbContext.UserRoles.AddRangeAsync(userRoles);
        await _labLinkDbContext.SaveChangesAsync();
    }

    async public Task<User?> GetByIdWithRoles(int id)
    {
        return await _labLinkDbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.UserId == id);
    }
}

