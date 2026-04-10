using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LabLinkBackend.Services
{
    public class SpecimenService : ISpecimenService
    {
        private readonly ISpecimenRepository _specimenRepository;

        public SpecimenService(ISpecimenRepository specimenRepository)
        {
            _specimenRepository = specimenRepository;
        }

        public async Task<Speciman> CreateSpecimenAsync(SpecimenCreateDTO dto)
        {
            // Enforce business rules: correct specimen type, container, status, etc.
            var specimen = new Speciman
            {
                OrderId = dto.OrderID,
                OrderItemId = dto.OrderItemId,
                SpecimenTypeId = dto.SpecimenTypeId,
                ContainerTypeId = dto.ContainerTypeId,
                CollectedBy = dto.CollectedBy,
                CollectedDate = dto.CollectedDate,
                RejectionReason = dto.RejectionReason,
                IsActive = true
            };

            // Save specimen and get generated ID
            var createdSpecimen = await _specimenRepository.AddSpecimenAsync(specimen);
            return createdSpecimen;
        }
        public async Task<bool> DeleteSpecimenAsync(int specimenId)
        {
            return await _specimenRepository.DeleteSpecimenAsync(specimenId);
        }
    }
}