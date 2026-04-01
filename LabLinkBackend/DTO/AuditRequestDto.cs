using System.ComponentModel.DataAnnotations;

namespace LabLinkBackend.DTO;

public class AuditRequestDto
{
    [Required, MaxLength(100)]
    public string Action { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string? Resource { get; set; }
    
    public string? Metadata { get; set; }
}
