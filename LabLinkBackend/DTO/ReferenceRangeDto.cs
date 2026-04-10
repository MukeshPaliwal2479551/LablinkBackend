using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabLinkBackend.DTO
{
    public class ReferenceRangeDto
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string? Gender { get; set; }
        public string? Range { get; set; }
    }
}