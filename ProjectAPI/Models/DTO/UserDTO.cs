using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Models.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
