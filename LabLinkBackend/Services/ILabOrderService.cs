using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface ILabOrderService
{
    Task<LabOrderResponseDto> CreateAsync(LabOrderDto dto);
    Task<List<LabOrderResponseDto>> GetAllAsync();
    Task<LabOrderResponseDto> UpdateAsync(int orderId, LabOrderDto dto);
    Task<LabOrderResponseDto> GetByIdAsync(int orderId);
    Task DeleteAsync(int orderId);
}