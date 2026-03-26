using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class PriceRef
{
    public int PriceId { get; set; }

    public int? TestId { get; set; }

    public int? PanelId { get; set; }

    public decimal Price { get; set; }

    public DateTime? EffectiveFrom { get; set; }

    public DateTime? EffectiveTo { get; set; }

    public bool IsActive { get; set; }

    public virtual Panel? Panel { get; set; }

    public virtual Test? Test { get; set; }
}
