using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
