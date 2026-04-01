using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabLinkBackend.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
    }
}