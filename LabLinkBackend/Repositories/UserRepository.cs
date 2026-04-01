using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LabLinkDbContext context;

        public UserRepository(LabLinkDbContext _context)
        {
            context = _context;
        }
        public async Task<User?> DeleteUser(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
                return null;
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return user;
        }
    public async Task<List<User>> GetUsersAsync(string? name, string? phone)
    {
        var query = context.Users.AsQueryable();
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(u =>
                u.Name.ToLower().Contains(name.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(phone))
        {
            query = query.Where(u =>
                u.Phone != null && u.Phone.Contains(phone));
        }
        return await query.ToListAsync();
        }
    }
}
