using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories;

public interface IOrderItemRepository
{
    Task AddAsync(OrderItem item);
    Task UpdateAsync(OrderItem item);
    Task<OrderItem?> GetByIdAsync(int orderItemId);
    Task<List<OrderItem>> GetByOrderIdAsync(int orderId);
    Task DeleteAsync(OrderItem item);
    Task<bool> ExistsAsync(int orderId, int? testId, int? panelId);
}