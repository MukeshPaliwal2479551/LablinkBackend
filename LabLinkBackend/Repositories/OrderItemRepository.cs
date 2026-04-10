using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly LabLinkDbContext _context;

    public OrderItemRepository(LabLinkDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(OrderItem item)
    {
        _context.OrderItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderItem item)
    {
        _context.OrderItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task<OrderItem?> GetByIdAsync(int orderItemId)
    {
        return await _context.OrderItems.FindAsync(orderItemId);
    }

    public async Task<List<OrderItem>> GetByOrderIdAsync(int orderId)
    {
        return await _context.OrderItems
            .Where(i => i.OrderId == orderId)
            .OrderByDescending(i => i.OrderItemId)
            .ToListAsync();
    }

    public async Task DeleteAsync(OrderItem item)
    {
        _context.OrderItems.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int orderId, int? testId, int? panelId)
    {
        if (!testId.HasValue == !panelId.HasValue)
            throw new ArgumentException(
                "Exactly one of testId or panelId is required.");

        return await _context.OrderItems.AnyAsync(i =>
            i.OrderId == orderId &&
            i.IsActive &&
            (
                (testId.HasValue && i.TestId == testId) ||
                (panelId.HasValue && i.PanelId == panelId)
            )
        );
    }
}