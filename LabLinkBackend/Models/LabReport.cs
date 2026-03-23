using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class LabReport
{
    public int ReportId { get; set; }

    public int OrderId { get; set; }

    public int? Version { get; set; }

    public string? ReportUri { get; set; }

    public DateTime? GeneratedDate { get; set; }

    public int? AuthorizedBy { get; set; }

    public DateTime? AuthorizedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual User? AuthorizedByNavigation { get; set; }

    public virtual LabOrder Order { get; set; } = null!;

    public virtual ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();

    public virtual ICollection<ReportDelivery> ReportDeliveries { get; set; } = new List<ReportDelivery>();
}
