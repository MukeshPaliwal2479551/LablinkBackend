using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly LabLinkDbContext _context;

    public PatientRepository(LabLinkDbContext context)
    {
        _context = context;
    }

    public async Task<Patient?> GetByIdAsync(int patientId)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == patientId);
    }

    public async Task<List<Patient>> GetAsync(string? name, string? phone)
    {
        var query = _context.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(p =>
                p.Name.ToLower().Contains(name.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(phone))
        {
            query = query.Where(p =>
                p.ContactInfo != null &&
                p.ContactInfo.Contains(phone));
        }

        return await query
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<bool> IsPatientExistAsync(
        string name,
        DateOnly dob,
        string phone)
    {
        return await _context.Patients.AnyAsync(p =>
            p.Name.ToLower() == name.ToLower() &&
            p.Dob == dob &&
            p.ContactInfo == phone
        );
    }

    public async Task<Patient> AddAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<Patient> UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task DeleteAsync(Patient patient)
    {
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
    }
}