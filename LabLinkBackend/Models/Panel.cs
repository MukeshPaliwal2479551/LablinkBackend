using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Panel
{
    public int PanelId { get; set; }

    public string PanelCode { get; set; } = null!;

    public string PanelName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<AppointmentItem> AppointmentItems { get; set; } = new List<AppointmentItem>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<PanelTest> PanelTests { get; set; } = new List<PanelTest>();

    public virtual ICollection<PriceRef> PriceRefs { get; set; } = new List<PriceRef>();
}
