using LabLinkBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabLinkBackend.Repositories
{
    public interface ISpecimenRepository
    {
        Task<Speciman> AddSpecimenAsync(Speciman specimen);
        Task<bool> DeleteSpecimenAsync(int specimenId);
        // Additional methods as needed
    }
}