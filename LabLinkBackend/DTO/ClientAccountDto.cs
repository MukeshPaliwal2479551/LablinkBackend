using System.ComponentModel.DataAnnotations;

namespace LabLinkBackend.DTO;

public class ClientAccountDto
{
    public int ClientId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Type { get; set; }

    public string? ContactInfo { get; set; }

    public string? Address { get; set; }

    public bool IsActive { get; set; }
}
