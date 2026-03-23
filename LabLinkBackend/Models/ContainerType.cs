using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class ContainerType
{
    public int ContainerTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? SpecimenType { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Speciman> Specimen { get; set; } = new List<Speciman>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
