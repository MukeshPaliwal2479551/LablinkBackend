using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class SpecimenType
{
    public int SpecimenTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Speciman> Specimen { get; set; } = new List<Speciman>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
