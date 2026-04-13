using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;
using Microsoft.AspNetCore.Http;

namespace LabLinkBackend.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _repository;
    private readonly IAuditLogService _auditLogService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderItemService(
        IOrderItemRepository repository,
        IAuditLogService auditLogService,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _auditLogService = auditLogService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OrderItemDto> CreateAsync(OrderItemDto dto)
    {
        try
        {
            var exists = await _repository.ExistsAsync(
                dto.OrderId,
                dto.TestId,
                dto.PanelId
            );

            if (exists)
                throw new InvalidOperationException(
                    "This test is already added to the order.");

            var item = new OrderItem
            {
                OrderId = dto.OrderId,
                TestId = dto.TestId,
                PanelId = dto.PanelId,
                Department = dto.Department,
                IsActive = dto.IsActive
            };

            await _repository.AddAsync(item);

            await _auditLogService.CreateLogAsync(new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "CREATE",
                Resource = "OrderItem",
                Metadata =
                    $"OrderItemId={item.OrderItemId}, OrderId={item.OrderId}, TestId={item.TestId}, PanelId={item.PanelId}"
            });

            return Map(item);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"An error occurred while creating OrderItem for OrderId={dto.OrderId}.",
                ex);
        }
    }

    public async Task<OrderItemDto> UpdateAsync(int orderItemId, OrderItemDto dto)
    {
        try
        {
            var item = await _repository.GetByIdAsync(orderItemId)
                ?? throw new InvalidOperationException("OrderItem not found.");

            item.TestId = dto.TestId;
            item.PanelId = dto.PanelId;
            item.Department = dto.Department;
            item.IsActive = dto.IsActive;

            await _repository.UpdateAsync(item);

            await _auditLogService.CreateLogAsync(new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "UPDATE",
                Resource = "OrderItem",
                Metadata =
                    $"OrderItemId={orderItemId}, OrderId={item.OrderId}, IsActive={item.IsActive}"
            });

            return Map(item);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"An error occurred while updating OrderItem (OrderItemId={orderItemId}).",
                ex);
        }
    }

    public async Task<OrderItemDto> GetByIdAsync(int orderItemId)
    {
        try
        {
            var item = await _repository.GetByIdAsync(orderItemId)
                ?? throw new InvalidOperationException("OrderItem not found.");

            return Map(item);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"An error occurred while retrieving OrderItem (OrderItemId={orderItemId}).",
                ex);
        }
    }

    public async Task<List<OrderItemDto>> GetByOrderIdAsync(int orderId)
    {
        try
        {
            var items = await _repository.GetByOrderIdAsync(orderId);
            return items.Select(Map).ToList();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"An error occurred while retrieving OrderItems for OrderId={orderId}.",
                ex);
        }
    }

    public async Task DeleteAsync(int orderItemId)
    {
        try
        {
            var item = await _repository.GetByIdAsync(orderItemId)
                ?? throw new InvalidOperationException("OrderItem not found.");

            await _repository.DeleteAsync(item);

            await _auditLogService.CreateLogAsync(new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "DELETE",
                Resource = "OrderItem",
                Metadata =
                    $"OrderItemId={orderItemId}, OrderId={item.OrderId}, TestId={item.TestId}, PanelId={item.PanelId}"
            });
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"An error occurred while deleting OrderItem (OrderItemId={orderItemId}).",
                ex);
        }
    }

    private static OrderItemDto Map(OrderItem item) =>
        new()
        {
            OrderItemId = item.OrderItemId,
            OrderId = item.OrderId,
            TestId = item.TestId,
            PanelId = item.PanelId,
            Department = item.Department,
            IsActive = item.IsActive
        };

    private int GetCurrentUserId()
    {
        var claimValue = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst("userId")?
            .Value;

        return int.TryParse(claimValue, out var userId) ? userId : 0;
    }
}