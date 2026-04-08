using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabLinkBackend.DTO;

public class LabOrderResponseDto
{
    public int OrderId { get; set; }
    public int PatientId { get; set; }
    public DateTime OrderDate { get; set; }
    public int Priority { get; set; }
    public bool IsActive { get; set; }
}
