using System;
using System.Collections.Generic;
using System.Linq;
using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Repositories;

public class AccessionRepository : IAccessionRepository
{
    private readonly LabLinkDbContext _context;

    public AccessionRepository(LabLinkDbContext context)
    {
        _context = context;
    }

    public async Task<Accession?> GetByIdAsync(int accessionId)
    {
        return await _context.Accessions
            .Include(a => a.Order)
            .FirstOrDefaultAsync(a => a.AccessionId == accessionId);
    }

    public async Task<Accession?> GetByOrderIdAsync(int orderId)
    {
        return await _context.Accessions
            .FirstOrDefaultAsync(a => a.OrderId == orderId && a.IsActive);
    }


    public async Task<List<Accession>> GetAllAsync()
    {
        return await _context.Accessions
            .Where(a => a.IsActive)
            .OrderByDescending(a => a.AccessionDate)
            .ToListAsync();
    }


    public async Task<bool> ExistsForOrderAsync(int orderId)
    {
        return await _context.Accessions
            .AnyAsync(a => a.OrderId == orderId && a.IsActive);
    }

    public async Task AddAsync(Accession accession)
    {
        _context.Accessions.Add(accession);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Accession accession)
    {
        _context.Accessions.Update(accession);
        await _context.SaveChangesAsync();
    }
}