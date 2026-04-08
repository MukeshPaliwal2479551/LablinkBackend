using System;
using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories;

public interface ILabOrderRepository
{
    Task AddAsync(LabOrder order);
    Task UpdateAsync(LabOrder order);
    Task<LabOrder?> GetByIdAsync(int orderId);
    Task<List<LabOrder>> GetAllAsync();
    Task DeleteAsync(LabOrder order);

}