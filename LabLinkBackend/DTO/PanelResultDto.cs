using System.ComponentModel.DataAnnotations;

namespace LabLinkBackend.DTO;

public class PanelResultDto
{
    public int PanelId { get; set; }

    public string PanelCode { get; set; } = string.Empty;

    public string PanelName { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}
