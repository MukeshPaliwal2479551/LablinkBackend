namespace LabLinkBackend.DTO;

public class PatientUpsertDto
{
    public int? PatientId { get; set; }
    public int UserId { get; set;}
    public string Name { get; set; } = string.Empty;
    public DateOnly? Dob { get; set; }
    public string? Gender { get; set; }
    public string? ContactInfo { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;
    public string? PrimaryPhysicianName { get; set; }
}