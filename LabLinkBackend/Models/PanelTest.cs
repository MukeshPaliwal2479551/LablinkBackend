using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class PanelTest
{
    public int PanelTestId { get; set; }

    public int PanelId { get; set; }

    public int TestId { get; set; }

    public string? ComponentsJson { get; set; }

    public bool IsActive { get; set; }

    public virtual Panel Panel { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;
}
