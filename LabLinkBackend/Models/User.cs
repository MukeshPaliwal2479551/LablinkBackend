using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool IsActive { get; set; }

    public string Password { get; set; }  = null!;

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime? UpdatedOn { get; set; }


    public virtual ICollection<Addendum> Addenda { get; set; } = new List<Addendum>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<CompetencyLog> CompetencyLogReviewers { get; set; } = new List<CompetencyLog>();

    public virtual ICollection<CompetencyLog> CompetencyLogStaffUsers { get; set; } = new List<CompetencyLog>();

    public virtual ICollection<LabOrder> LabOrders { get; set; } = new List<LabOrder>();

    public virtual ICollection<LabReport> LabReports { get; set; } = new List<LabReport>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<PathologyReview> PathologyReviews { get; set; } = new List<PathologyReview>();

    public virtual Patient? PatientUser { get; set; }

    public virtual ICollection<ResultEntry> ResultEntries { get; set; } = new List<ResultEntry>();

    public virtual ICollection<Speciman> Specimen { get; set; } = new List<Speciman>();

    public virtual ICollection<TechValidation> TechValidations { get; set; } = new List<TechValidation>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
  
  