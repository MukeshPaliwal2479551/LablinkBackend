using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class InterfaceType
{
    public int InterfaceTypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<InstrumentRef> InstrumentRefs { get; set; } = new List<InstrumentRef>();
}
