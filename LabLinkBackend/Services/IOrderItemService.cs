using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IOrderItemService
{
    Task<OrderItemResponseDto> CreateAsync(OrderItemDto dto);
    Task<OrderItemResponseDto> UpdateAsync(int orderItemId, OrderItemDto dto);
    Task<OrderItemResponseDto> GetByIdAsync(int orderItemId);
    Task<List<OrderItemResponseDto>> GetByOrderIdAsync(int orderId);
    Task DeleteAsync(int orderItemId);
}