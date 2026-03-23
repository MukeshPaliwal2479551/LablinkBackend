using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Speciman
{
    public int SpecimenId { get; set; }

    public int OrderId { get; set; }

    public int OrderItemId { get; set; }

    public int? SpecimenTypeId { get; set; }

    public int? ContainerTypeId { get; set; }

    public int? CollectedBy { get; set; }

    public DateTime? CollectedDate { get; set; }

    public string? RejectionReason { get; set; }

    public bool IsActive { get; set; }

    public virtual User? CollectedByNavigation { get; set; }

    public virtual ContainerType? ContainerType { get; set; }

    public virtual LabOrder Order { get; set; } = null!;

    public virtual OrderItem OrderItem { get; set; } = null!;

    public virtual SpecimenType? SpecimenType { get; set; }
}
