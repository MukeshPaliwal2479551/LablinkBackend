using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Worklist
{
    public int WorklistId { get; set; }

    public string? Section { get; set; }

    public int? InstrumentId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Items { get; set; }

    public bool IsActive { get; set; }

    public virtual InstrumentRef? Instrument { get; set; }
}
