using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;
using Microsoft.AspNetCore.Http;

namespace LabLinkBackend.Services;

public class LabOrderService : ILabOrderService
{
    private readonly ILabOrderRepository _repository;
    private readonly IAuditLogService _auditLogService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LabOrderService(
        ILabOrderRepository repository,
        IAuditLogService auditLogService,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _auditLogService = auditLogService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<LabOrderDto> CreateAsync(LabOrderDto dto)
    {
        try
        {
            var order = new LabOrder
            {
                PatientId = dto.PatientId,
                ClientId = dto.ClientId,
                Priority = dto.Priority,
                OrderDate = DateTime.UtcNow,
                IsActive = dto.IsActive
            };

            await _repository.AddAsync(order);

            await _auditLogService.CreateLogAsync(new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "CREATE",
                Resource = "LabOrder",
                Metadata = $"OrderId={order.OrderId}, PatientId={order.PatientId}"
            });

            return Map(order);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                "An error occurred while creating LabOrder.", ex);
        }
    }

    public async Task<LabOrderDto> UpdateAsync(int orderId, LabOrderDto dto)
    {
        try
        {
            var order = await _repository.GetByIdAsync(orderId)
                ?? throw new InvalidOperationException("LabOrder not found.");

            order.Priority = dto.Priority;
            order.ClientId = dto.ClientId;
            order.IsActive = dto.IsActive;

            await _repository.UpdateAsync(order);

            await _auditLogService.CreateLogAsync(new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "UPDATE",
                Resource = "LabOrder",
                Metadata = $"OrderId={orderId}, IsActive={dto.IsActive}"
            });

            return Map(order);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"An error occurred while updating LabOrder (OrderId={orderId}).", ex);
        }
    }

    public async Task<List<LabOrderDto>> GetAllAsync()
    {
        try
        {
            var orders = await _repository.GetAllAsync();
            return orders.Select(Map).ToList();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                "An error occurred while retrieving LabOrders.", ex);
        }
    }

    public async Task<LabOrderDto> GetByIdAsync(int orderId)
    {
        try
        {
            var order = await _repository.GetByIdAsync(orderId)
                ?? throw new InvalidOperationException("LabOrder not found.");

            return Map(order);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"An error occurred while retrieving LabOrder (OrderId={orderId}).", ex);
        }
    }

    public async Task<List<LabOrderDto>> SearchAsync(
        int? patientId,
        DateTime? orderDate)
    {
        try
        {
            var orders = await _repository.SearchAsync(patientId, orderDate);
            return orders.Select(Map).ToList();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                "An error occurred while searching LabOrders.", ex);
        }
    }

    public async Task DeleteAsync(int orderId)
    {
        try
        {
            var order = await _repository.GetByIdAsync(orderId)
                ?? throw new InvalidOperationException("LabOrder not found.");

            order.IsActive = false;
            await _repository.DeleteAsync(order);

            await _auditLogService.CreateLogAsync(new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "DELETE",
                Resource = "LabOrder",
                Metadata = $"OrderId={orderId}"
            });
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"An error occurred while deleting LabOrder (OrderId={orderId}).", ex);
        }
    }
    private static LabOrderDto Map(LabOrder order) =>
        new()
        {
            OrderId = order.OrderId,
            PatientId = order.PatientId,
            ClientId = order.ClientId,
            Priority = order.Priority,
            OrderDate = order.OrderDate ?? DateTime.UtcNow,
            IsActive = order.IsActive
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