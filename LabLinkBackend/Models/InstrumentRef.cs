using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class InstrumentRef
{
    public int InstrumentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Model { get; set; }

    public string? Section { get; set; }

    public int? InterfaceTypeId { get; set; }

    public bool IsActive { get; set; }

    public virtual InterfaceType? InterfaceType { get; set; }

    public virtual ICollection<Qcrecord> Qcrecords { get; set; } = new List<Qcrecord>();

    public virtual ICollection<Worklist> Worklists { get; set; } = new List<Worklist>();
}
