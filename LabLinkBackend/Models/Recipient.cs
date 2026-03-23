using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Recipient
{
    public int RecipientId { get; set; }

    public int ReportId { get; set; }

    public string? RecipientType { get; set; }

    public int? RecipientRefId { get; set; }

    public bool IsActive { get; set; }

    public virtual LabReport Report { get; set; } = null!;
}
