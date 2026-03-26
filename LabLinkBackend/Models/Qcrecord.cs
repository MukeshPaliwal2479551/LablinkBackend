using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Qcrecord
{
    public int QcId { get; set; }

    public int InstrumentId { get; set; }

    public int TestId { get; set; }

    public string? ControlLevel { get; set; }

    public string? ResultValue { get; set; }

    public string? Units { get; set; }

    public DateTime? RunDate { get; set; }

    public string? RuleFlags { get; set; }

    public string Status { get; set; } = null!;

    public virtual InstrumentRef Instrument { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;
}
