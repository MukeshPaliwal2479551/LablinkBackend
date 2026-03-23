using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Visit
{
    public int VisitTypeId { get; set; }

    public string VisitName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
