using System;
using System.Collections.Generic;
using System.Linq;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IAccessionService
{
    Task<AccessionDto?> CreateAsync(int orderId, string? section);
    Task<AccessionDto?> GetByIdAsync(int accessionId);
    Task<AccessionDto?> GetByOrderIdAsync(int orderId);
    Task<AccessionDto?> UpdateSectionAsync(int accessionId, string? section);
    Task<bool> CancelAsync(int accessionId);
}