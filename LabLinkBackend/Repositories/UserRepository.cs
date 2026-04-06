
using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Repositories;

public class UserRepository : IUserRepository
{
    LabLinkDbContext _labLinkDbContext;
    public UserRepository(LabLinkDbContext labLinkDbContext)
    {
        _labLinkDbContext = labLinkDbContext;
    }
    public async Task<bool> Delete(int id)
    {
        var user = await _labLinkDbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
        if (user == null)
            return false;
        _labLinkDbContext.Users.Remove(user);
        await _labLinkDbContext.SaveChangesAsync();
        return true;
        }
    public async Task<List<User>> GetUsersAsync(string name, string phone)
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

