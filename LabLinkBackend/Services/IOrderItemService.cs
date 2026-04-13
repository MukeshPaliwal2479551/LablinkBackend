using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IOrderItemService
{
    Task<OrderItemDto> CreateAsync(OrderItemDto dto);
    Task<OrderItemDto> UpdateAsync(int orderItemId, OrderItemDto dto);
    Task<OrderItemDto> GetByIdAsync(int orderItemId);
    Task<List<OrderItemDto>> GetByOrderIdAsync(int orderId);
    Task DeleteAsync(int orderItemId);
}
