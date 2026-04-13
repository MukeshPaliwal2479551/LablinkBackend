using System;
using System.Collections.Generic;
using System.Linq;
using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories;

public interface IAccessionRepository
{
    Task<Accession?> GetByIdAsync(int accessionId);
    Task<Accession?> GetByOrderIdAsync(int orderId);
    Task<bool> ExistsForOrderAsync(int orderId);
    Task<List<Accession>> GetAllAsync();
    Task AddAsync(Accession accession);
    Task UpdateAsync(Accession accession);
}