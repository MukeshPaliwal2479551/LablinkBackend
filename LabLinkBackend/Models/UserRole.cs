using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime? AssignedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual Roles Roles { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
