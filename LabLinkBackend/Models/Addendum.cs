using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Addendum
{
    public int AddendumId { get; set; }

    public int OrderId { get; set; }

    public int UserId { get; set; }

    public string AddendumText { get; set; } = null!;

    public DateTime? AddedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual LabOrder Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
