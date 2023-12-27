using AutoMapper;
using ProjectAPI.Models;
using ProjectAPI.Models.DTO;

namespace ProjectAPI
{
    public class MapperProfile : Profile
    {
        public MapperProfile() {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO,User>();
            CreateMap<PostDTO, Post>();
            CreateMap<Post, PostDTO>();
            CreateMap<Post,AddPostDTO>();
            CreateMap<AddPostDTO, Post>();
        }
    }
}
