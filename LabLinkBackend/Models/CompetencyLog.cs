using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class CompetencyLog
{
    public int CompetencyLogId { get; set; }

    public int StaffUserId { get; set; }

    public string? CompetencyType { get; set; }

    public DateTime? CompletedDate { get; set; }

    public int ReviewerId { get; set; }

    public bool IsActive { get; set; }

    public virtual User Reviewer { get; set; } = null!;

    public virtual User StaffUser { get; set; } = null!;
}
