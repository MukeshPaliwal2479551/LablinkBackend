using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class PathologyReview
{
    public int ReviewId { get; set; }

    public int OrderId { get; set; }

    public int UserId { get; set; }

    public string? Notes { get; set; }

    public DateTime? ReviewDate { get; set; }

    public bool IsActive { get; set; }

    public virtual LabOrder Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
