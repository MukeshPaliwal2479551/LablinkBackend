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
        return await _context.LabOrders
            .FirstOrDefaultAsync(o => o.OrderId == orderId && o.IsActive);
    }

    public async Task<List<LabOrder>> GetAsync(int? patientId, DateTime? orderDate)
    {
        var query = _context.LabOrders
            .Where(o => o.IsActive)
            .AsQueryable();

        if (patientId.HasValue)
        {
            query = query.Where(o => o.PatientId == patientId.Value);
        }

        if (orderDate.HasValue)
        {
            var date = orderDate.Value.Date;

            query = query.Where(o =>
                o.OrderDate.HasValue &&
                o.OrderDate.Value.Date == date);
        }

        return await query
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }
    public async Task DeleteAsync(LabOrder order)
    {
        _context.LabOrders.Update(order);
        await _context.SaveChangesAsync();
    }
}