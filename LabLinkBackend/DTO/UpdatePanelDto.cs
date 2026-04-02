using System.ComponentModel.DataAnnotations;

namespace LabLinkBackend.DTO;

public class UpdatePanelDto
{
    [Required(ErrorMessage = "Panel ID is required")]
    public int PanelId { get; set; }

    [MaxLength(255, ErrorMessage = "Panel Name cannot exceed 255 characters")]
    public string? PanelName { get; set; }

    [MinLength(1, ErrorMessage = "Panel must contain at least one test")]
    public List<int> TestIds { get; set; } = new List<int>();

    public bool? IsActive { get; set; }
}

