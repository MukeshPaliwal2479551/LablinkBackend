using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class AppointmentItem
{
    public int AppItemId { get; set; }

    public int AppointmentId { get; set; }

    public int? TestId { get; set; }

    public int? PanelId { get; set; }

    public int Priority { get; set; }

    public string? Instructions { get; set; }

    public bool IsActive { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual Panel? Panel { get; set; }

    public virtual Test? Test { get; set; }
}
