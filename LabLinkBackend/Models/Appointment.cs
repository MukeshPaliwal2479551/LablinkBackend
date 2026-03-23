using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public DateTime BookedDateTime { get; set; }

    public int? VisitTypeId { get; set; }

    public string? Address { get; set; }

    public int? PhlebotomistId { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AppointmentItem> AppointmentItems { get; set; } = new List<AppointmentItem>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual User? Phlebotomist { get; set; }

    public virtual Visit? VisitType { get; set; }
}
