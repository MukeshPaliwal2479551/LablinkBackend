namespace LabLinkBackend.DTO;

public class AppointmentDto
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public DateTime BookedDateTime { get; set; }
    public string? Address { get; set; }
    public int? VisitTypeId { get; set; }
    public int? PhlebotomistId { get; set; }
    public bool IsActive { get; set; }
}
