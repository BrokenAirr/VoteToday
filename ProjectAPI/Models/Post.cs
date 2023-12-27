using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public int UserId {get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int Trues { get; set; } = 0;
        public int Falses { get; set; } = 0;
    }
}
