using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class ClientAccount
{
    public int ClientId { get; set; }

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public string? ContactInfo { get; set; }

    public string? Address { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<InvoiceRef> InvoiceRefs { get; set; } = new List<InvoiceRef>();

    public virtual ICollection<LabOrder> LabOrders { get; set; } = new List<LabOrder>();
}
