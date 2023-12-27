using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(8)]
        public string Password { get; set; }
    }
}
