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

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _labLinkDbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
        if (user == null)
            return false;
        _labLinkDbContext.Users.Remove(user);
        await _labLinkDbContext.SaveChangesAsync();
        return true;
        }
    public async Task<List<User>> GetUsersAsync(string? name, string? phone)
        {
        var query = _labLinkDbContext.Users.AsQueryable();
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(u => u.Name.ToLower().Contains(name.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(phone))
        {
            query = query.Where(u => u.Phone != null && u.Phone.Contains(phone));
        }
        return await query.ToListAsync();
        }
}
    
