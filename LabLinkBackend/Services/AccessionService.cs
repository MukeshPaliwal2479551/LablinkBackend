using System;
using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;

namespace LabLinkBackend.Services;

public class AccessionService : IAccessionService
{
    private readonly IAccessionRepository _accessionRepo;
    private readonly ILabOrderRepository _orderRepo;

    public AccessionService(
        IAccessionRepository accessionRepo,
        ILabOrderRepository orderRepo)
    {
        _accessionRepo = accessionRepo;
        _orderRepo = orderRepo;
    }

    public async Task<AccessionDto?> CreateAsync(int orderId, string? section)
    {
        try
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null || !order.IsActive)
                return null;

            var exists = await _accessionRepo.ExistsForOrderAsync(orderId);
            if (exists)
                throw new InvalidOperationException(
                    "Accession already exists for this order.");

            var accession = new Accession
            {
                OrderId = orderId,
                AccessionNumber = GenerateAccessionNumber(),
                AccessionDate = DateTime.UtcNow,
                Section = section,
                IsActive = true
            };

            await _accessionRepo.AddAsync(accession);
            return Map(accession);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"Failed to create accession for OrderId={orderId}.", ex);
        }
    }

    public async Task<AccessionDto?> GetByIdAsync(int accessionId)
    {
        try
        {
            var accession = await _accessionRepo.GetByIdAsync(accessionId);
            return accession == null ? null : Map(accession);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"Failed to retrieve accession (AccessionId={accessionId}).", ex);
        }
    }

    public async Task<AccessionDto?> GetByOrderIdAsync(int orderId)
    {
        try
        {
            var accession = await _accessionRepo.GetByOrderIdAsync(orderId);
            return accession == null ? null : Map(accession);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"Failed to retrieve accession for OrderId={orderId}.", ex);
        }
    }
    
    public async Task<AccessionDto?> UpdateSectionAsync(int accessionId, string? section)
    {
        try
        {
            var accession = await _accessionRepo.GetByIdAsync(accessionId);
            if (accession == null || !accession.IsActive)
                return null;

            accession.Section = section;
            await _accessionRepo.UpdateAsync(accession);

            return Map(accession);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"Failed to update section for AccessionId={accessionId}.", ex);
        }
    }

    public async Task<bool> CancelAsync(int accessionId)
    {
        try
        {
            var accession = await _accessionRepo.GetByIdAsync(accessionId);
            if (accession == null || !accession.IsActive)
                return false;

            accession.IsActive = false;
            await _accessionRepo.UpdateAsync(accession);

            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                $"Failed to cancel accession (AccessionId={accessionId}).", ex);
        }
    }

    private static AccessionDto Map(Accession accession) =>
        new()
        {
            AccessionId = accession.AccessionId,
            OrderId = accession.OrderId,
            AccessionNumber = accession.AccessionNumber,
            AccessionDate = accession.AccessionDate,
            Section = accession.Section,
            IsActive = accession.IsActive
        };

    private static string GenerateAccessionNumber()
    {
        return $"ACC-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid():N}"
            .Substring(0, 20);
    }
}