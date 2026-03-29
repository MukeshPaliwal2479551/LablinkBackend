namespace LabLinkBackend.DTO;

public class PatientResponseDto
{
    public int PatientId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly? Dob { get; set; }
    public string? Gender { get; set; }
    public string? ContactInfo { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public string? PrimaryPhysicianName { get; set; }
}