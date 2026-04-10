namespace LabLinkBackend.DTO;

public class LabOrderDto
{
    public int PatientId { get; set; }
    public int? ClientId { get; set; }
    public int Priority { get; set; }
    public bool IsActive { get; set; } = true;
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
}