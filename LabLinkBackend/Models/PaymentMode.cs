using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class PaymentMode
{
    public int PaymentModeId { get; set; }

    public string ModeName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<PaymentRef> PaymentRefs { get; set; } = new List<PaymentRef>();
}
