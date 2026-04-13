using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface ILabOrderService
{
    Task<LabOrderDto> CreateAsync(LabOrderDto dto);
    Task<List<LabOrderDto>> GetAsync(int? patientId, DateTime? orderDate);
    Task<LabOrderDto> GetByIdAsync(int orderId);
    Task<LabOrderDto> UpdateAsync(int orderId, LabOrderDto dto);
    Task DeleteAsync(int orderId);
}