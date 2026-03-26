using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Flag
{
    public int FlagId { get; set; }

    public string FlagType { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ResultEntry> ResultEntries { get; set; } = new List<ResultEntry>();
}
