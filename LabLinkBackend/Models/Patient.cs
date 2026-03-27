using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? Dob { get; set; }

    public string? Gender { get; set; }

    public string? ContactInfo { get; set; }

    public string? Address { get; set; }

    public string? PrimaryPhysicianName { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<InvoiceRef> InvoiceRefs { get; set; } = new List<InvoiceRef>();

    public virtual ICollection<LabOrder> LabOrders { get; set; } = new List<LabOrder>();

    public virtual User? PrimaryPhysician { get; set; }

    public virtual User User { get; set; } = null!;
}
