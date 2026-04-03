using System;
using System.Collections.Generic;

namespace LabLinkBackend.Models;

public partial class Roles
{
    public int RoleId { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
