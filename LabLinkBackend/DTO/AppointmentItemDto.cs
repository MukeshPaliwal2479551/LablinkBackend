using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabLinkBackend.DTO
{
    public class AppointmentItemDto
    {
        [Required]
        public int AppointmentId { get; set; }
        
        public int? TestId { get; set; }
        public int? PanelId { get; set; }

        [Required]
        [Range(0, 1)] // Assuming priority from 1 to 10
        public int Priority { get; set; }

        [MaxLength(500)]
        public string? Instructions { get; set; }

        public bool? IsActive { get; set; }
    }

    public class AppointmentItemUpdateDto
    {
        [Required]
        [Range(0, 1)] // Assuming priority from 1 to 10
        public int Priority { get; set; }

        [MaxLength(500)]
        public string? Instructions { get; set; }

        public bool? IsActive { get; set; }
    }
}

