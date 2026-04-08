using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Repositories;

public class LabOrderRepository : ILabOrderRepository
{
    private readonly LabLinkDbContext _context;

    public LabOrderRepository(LabLinkDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LabOrder order)
    {
        _context.LabOrders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LabOrder order)
    {
        _context.LabOrders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task<LabOrder?> GetByIdAsync(int orderId)
    {
        return await _context.LabOrders.FindAsync(orderId);
    }

    public async Task<List<LabOrder>> GetAllAsync()
    {
        return await _context.LabOrders
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }
    
    public async Task DeleteAsync(LabOrder order)
    {
        _context.LabOrders.Update(order);
        await _context.SaveChangesAsync();
    }



}