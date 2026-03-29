using LabLinkBackend.Data;
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

    public async Task<Patient?> GetByIdAsync(int patientId) =>
        await _context.Patients.FindAsync(patientId);

    public async Task<Patient?> GetByNameDobPhoneAsync(
        string name,
        DateOnly? dob,
        string? phone) =>
        await _context.Patients.FirstOrDefaultAsync(p =>
            p.Name.ToLower() == name.ToLower() &&
            p.Dob == dob &&
            p.ContactInfo == phone);

    public async Task<Patient> AddAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        return patient;
    }

    public async Task<Patient> UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        return patient;
    }

    public async Task<int> SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}