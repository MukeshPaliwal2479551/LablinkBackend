namespace LabLinkBackend.DTO;

public class OrderItemResponseDto
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int? TestId { get; set; }
    public int? PanelId { get; set; }
    public string? Department { get; set; }
    public bool IsActive { get; set; }
}