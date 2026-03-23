using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class LabOrder
{
    public int OrderId { get; set; }

    public int PatientId { get; set; }

    public int? ClientId { get; set; }

    public int? OrderedByUserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int Priority { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Accession> Accessions { get; set; } = new List<Accession>();

    public virtual ICollection<Addendum> Addenda { get; set; } = new List<Addendum>();

    public virtual ClientAccount? Client { get; set; }

    public virtual ICollection<InvoiceRef> InvoiceRefs { get; set; } = new List<InvoiceRef>();

    public virtual ICollection<LabReport> LabReports { get; set; } = new List<LabReport>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? OrderedByUser { get; set; }

    public virtual ICollection<PathologyReview> PathologyReviews { get; set; } = new List<PathologyReview>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Speciman> Specimen { get; set; } = new List<Speciman>();
}
