using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class ResultEntry
{
    public int ResultId { get; set; }

    public int OrderItemId { get; set; }

    public int TestId { get; set; }

    public string? Analyte { get; set; }

    public string? Value { get; set; }

    public string? Units { get; set; }

    public int FlagId { get; set; }

    public int? EnteredBy { get; set; }

    public DateTime? EnteredDate { get; set; }

    public string? Source { get; set; }

    public bool IsActive { get; set; }

    public virtual User? EnteredByNavigation { get; set; }

    public virtual Flag Flag { get; set; } = null!;

    public virtual OrderItem OrderItem { get; set; } = null!;

    public virtual ICollection<TechValidation> TechValidations { get; set; } = new List<TechValidation>();

    public virtual Test Test { get; set; } = null!;
}
