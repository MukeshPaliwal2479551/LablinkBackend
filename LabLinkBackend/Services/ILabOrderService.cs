using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface ILabOrderService
{
    Task<LabOrderDto> CreateAsync(LabOrderDto dto);
    Task<List<LabOrderDto>> GetAllAsync();
    Task<LabOrderDto> UpdateAsync(int orderId, LabOrderDto dto);
    Task<LabOrderDto> GetByIdAsync(int orderId);
    Task DeleteAsync(int orderId);
    Task<List<LabOrderDto>> SearchAsync(int? patientId, DateTime? orderDate);
}