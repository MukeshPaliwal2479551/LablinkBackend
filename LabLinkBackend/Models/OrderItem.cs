using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int? TestId { get; set; }

    public int? PanelId { get; set; }

    public string? Department { get; set; }

    public bool IsActive { get; set; }

    public virtual LabOrder Order { get; set; } = null!;

    public virtual Panel? Panel { get; set; }

    public virtual ICollection<ResultEntry> ResultEntries { get; set; } = new List<ResultEntry>();

    public virtual ICollection<Speciman> Specimen { get; set; } = new List<Speciman>();

    public virtual Test? Test { get; set; }
}
