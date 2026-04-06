using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabLinkBackend.DTO
{
    public class UserRoleUpdateDTO
    {
        [Required]
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}