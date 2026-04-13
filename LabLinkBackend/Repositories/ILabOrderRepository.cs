using System;
using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories;

public interface ILabOrderRepository
{
    Task AddAsync(LabOrder order);
    Task UpdateAsync(LabOrder order);
    Task<LabOrder?> GetByIdAsync(int orderId);
    Task DeleteAsync(LabOrder order);
    Task<List<LabOrder>> GetAsync(int? patientId, DateTime? orderDate);

}