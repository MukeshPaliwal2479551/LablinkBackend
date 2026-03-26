using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class ReportDelivery
{
    public int DeliveryId { get; set; }

    public int ReportId { get; set; }

    public string Channel { get; set; } = null!;

    public string? RecipientType { get; set; }

    public int? RecipientId { get; set; }

    public DateTime? DeliveredDate { get; set; }

    public bool IsActive { get; set; }

    public virtual LabReport Report { get; set; } = null!;
}
