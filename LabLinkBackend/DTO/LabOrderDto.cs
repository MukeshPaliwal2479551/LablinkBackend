using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabLinkBackend.DTO;

public class LabOrderDto
{
    public int PatientId { get; set; }
    public int? ClientId { get; set; }
    public int OrderedByUserId { get; set; }
    public int Priority { get; set; }
    public bool IsActive { get; set; } = true;
}