using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class InvoiceRef
{
    public int InvoiceId { get; set; }

    public int OrderId { get; set; }

    public int PatientId { get; set; }

    public int? ClientId { get; set; }

    public decimal Amount { get; set; }

    public decimal? Tax { get; set; }

    public decimal Total { get; set; }

    public DateTime? GeneratedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ClientAccount? Client { get; set; }

    public virtual LabOrder Order { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<PaymentRef> PaymentRefs { get; set; } = new List<PaymentRef>();
}
