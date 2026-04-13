using System;
using System.Collections.Generic;
using System.Linq;
namespace LabLinkBackend.DTO;

public class AccessionDto
{
    public int AccessionId { get; set; }
    public int OrderId { get; set; }
    public string AccessionNumber { get; set; } = string.Empty;
    public DateTime? AccessionDate { get; set; }
    public string? Section { get; set; }
    public bool IsActive { get; set; } = true;
}