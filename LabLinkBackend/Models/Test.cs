using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Test
{
    public int TestId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? DepartmentId { get; set; }
 
    public int? MethodId { get; set; }

    public int? SpecimenTypeId { get; set; }

    public int ContainerTypeId { get; set; }

    public double? VolumeReq { get; set; }

    public int? Units { get; set; }

    public int MaxNormalValue { get; set; }

    public int MinNormalValue { get; set; }

    public int? TattargetMinutes { get; set; }

    public string? RefRangeJson { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AppointmentItem> AppointmentItems { get; set; } = new List<AppointmentItem>();

    public virtual ContainerType ContainerType { get; set; } = null!;

    public virtual Department? Department { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<PanelTest> PanelTests { get; set; } = new List<PanelTest>();

    public virtual ICollection<PriceRef> PriceRefs { get; set; } = new List<PriceRef>();

    public virtual ICollection<Qcrecord> Qcrecords { get; set; } = new List<Qcrecord>();

    public virtual ICollection<ResultEntry> ResultEntries { get; set; } = new List<ResultEntry>();

    public virtual SpecimenType? SpecimenType { get; set; }
}
