using System.Collections.Generic;

namespace LabLinkBackend.DTO;

public class ClientListResult
{
    public bool Result { get; set; }
    public string? Message { get; set; }
    public List<ClientAccountDto> Clients { get; set; } = new();
}
