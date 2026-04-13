using System.ComponentModel.DataAnnotations;

namespace LabLinkBackend.DTO
{
    public class UserRegisterDTO
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
        [MinLength(8)]
        [MaxLength(20)]
        public string Password { get; set; } = null!;

        [Required]
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}