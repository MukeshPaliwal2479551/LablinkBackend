using System.ComponentModel.DataAnnotations;

namespace LabLinkBackend.DTO;

public class CreatePanelDto
{
    [Required(ErrorMessage = "Panel Code is required")]
    [MaxLength(50, ErrorMessage = "Panel Code cannot exceed 50 characters")]
    public string PanelCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Panel Name is required")]
    [MaxLength(255, ErrorMessage = "Panel Name cannot exceed 255 characters")]
    public string PanelName { get; set; } = string.Empty;

    [Required(ErrorMessage = "At least one Test ID is required")]
    [MinLength(1, ErrorMessage = "Panel must contain at least one test")]
    public List<int> TestIds { get; set; } = new List<int>();

    public bool IsActive { get; set; } = true;
}
