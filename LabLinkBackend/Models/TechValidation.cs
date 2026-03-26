using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class TechValidation
{
    public int TvId { get; set; }

    public int ResultId { get; set; }

    public int UserId { get; set; }

    public string? DeltaCheckJson { get; set; }

    public DateTime? ValidationDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ResultEntry Result { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
