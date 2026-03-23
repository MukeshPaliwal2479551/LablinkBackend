using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class LabReportPack
{
    public int LabReportPackId { get; set; }

    public string? Scope { get; set; }

    public string? Metrics { get; set; }

    public DateTime? GeneratedDate { get; set; }
}
