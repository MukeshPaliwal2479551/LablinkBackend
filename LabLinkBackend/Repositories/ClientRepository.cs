using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LabLinkBackend.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly LabLinkDbContext _context;

    public ClientRepository(LabLinkDbContext context)
    {
        _context = context;
    }

    public async Task<ClientListResult> GetAllAsync()
    {
        var result = new ClientListResult();
        try
        {
            result.Clients = await _context.ClientAccounts
                .Where(c => c.IsActive)
                .Select(c => new ClientAccountDto
                {
                    ClientId = c.ClientId,
                    Name = c.Name,
                    Type = c.Type,
                    ContactInfo = c.ContactInfo,
                    Address = c.Address,
                    IsActive = c.IsActive
                })
                .ToListAsync();
            result.Result = true;
        }
        catch (Exception ex)
        {
            result.Result = false;
            result.Message = $"Unexpected error: {ex.Message}";
        }
        return result;
    }
}
