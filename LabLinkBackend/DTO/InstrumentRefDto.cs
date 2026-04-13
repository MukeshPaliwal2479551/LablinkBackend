namespace LabLinkBackend.DTO;

public class InstrumentRefDto
{
    public int InstrumentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Model { get; set; }
    public string? Section { get; set; }
    public int? InterfaceTypeId { get; set; }
    public bool IsActive { get; set; } = true;
}
