using LabLinkBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabLinkBackend.Repositories
{
    public class SpecimenRepository : ISpecimenRepository
    {
        private readonly LabLinkDbContext _context;

        public SpecimenRepository(LabLinkDbContext context)
        {
            _context = context;
        }

        public async Task<Speciman> AddSpecimenAsync(Speciman specimen)
        {
            _context.Specimen.Add(specimen);
            await _context.SaveChangesAsync();
            return specimen;
        }
        public async Task<bool> DeleteSpecimenAsync(int specimenId)
        {
            var specimen = await _context.Specimen.FindAsync(specimenId);
            if (specimen == null)
                return false;
            _context.Specimen.Remove(specimen);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}