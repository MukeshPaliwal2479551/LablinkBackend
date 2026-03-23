using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class PaymentRef
{
    public int PaymentId { get; set; }

    public int InvoiceId { get; set; }

    public decimal Amount { get; set; }

    public int ModeId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public bool IsActive { get; set; }

    public virtual InvoiceRef Invoice { get; set; } = null!;

    public virtual PaymentMode Mode { get; set; } = null!;
}
