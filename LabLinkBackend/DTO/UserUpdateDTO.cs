using System.ComponentModel.DataAnnotations;

namespace LabLinkBackend.DTO
{
    public class UserUpdateDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }

        [MinLength(8)]
        [MaxLength(20)]
        public string? Password { get; set; }


    }
}