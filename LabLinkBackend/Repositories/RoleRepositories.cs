using LabLinkBackend.Data;
using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;
namespace LabLinkBackend.Data;
public class RoleRepository : IRoleRepository
{
    private readonly LabLinkDbContext _context;

    public RoleRepository(LabLinkDbContext context)
    {
        _context = context;
    }

    public async Task<Roles> CreateRoleAsync(Roles role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<IEnumerable<Roles>> GetAllRolesAsync()
        => await _context.Roles.ToListAsync();

    public async Task<Roles?> GetRoleByIdAsync(int roleId)
        => await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);
}