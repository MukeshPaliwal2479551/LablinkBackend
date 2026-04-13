using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabLinkBackend.Services
{
    public interface ISpecimenService
    {
        Task<Speciman> CreateSpecimenAsync(SpecimenCreateDTO dto);
        Task<bool> DeleteSpecimenAsync(int specimenId);
        // Additional methods as needed
    }
}