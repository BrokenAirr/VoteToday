using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Models.DTO
{
    public class PostDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
