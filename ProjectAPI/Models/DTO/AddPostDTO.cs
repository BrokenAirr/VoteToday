namespace ProjectAPI.Models.DTO
{
    public class AddPostDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public IFormFile File { get; set; }
        public UserDTO User { get; set; }
    }
}
