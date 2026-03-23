using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Accession
{
    public int AccessionId { get; set; }

    public int OrderId { get; set; }

    public string AccessionNumber { get; set; } = null!;

    public DateTime? AccessionDate { get; set; }

    public string? Section { get; set; }

    public bool IsActive { get; set; }

    public virtual LabOrder Order { get; set; } = null!;
}
