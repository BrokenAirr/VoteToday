using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;
using ProjectAPI.Models.DTO;
using System.Net;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        Mydatabase context;
        Response response;
        IMapper mapper;
        public MyController(Mydatabase _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
            response = new Response();
        }
        private async Task<Response> LoginValid(UserDTO _user)
        {
            try
            {
                User? user = await context.Users.FirstOrDefaultAsync(context => context.Email == _user.Email && context.Password == _user.Password);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Incorrect Email or Password";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Result = null;
                    return response;
                }
                response.IsSuccess = true;
                response.ErrorMessage = null;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = user;

            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        [HttpGet("Users")]
        public async Task<ActionResult<Response>> GetUsers()
        {
            try
            {
                List<User> users = await context.Users.ToListAsync();
                if(users == null)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Nothing Found";
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Result = null;
                    return NotFound(response);
                }
                response.IsSuccess = true;
                response.ErrorMessage = null;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = users;

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(response);
        }
        [HttpPost("Registration")]
        public async Task<ActionResult<Response>> Registration(UserDTO _user)
        {
            try
            {
                User user = mapper.Map<User>(_user);
                if (user.Email.Length > 50 || user.Password.Length > 8 || user.Email == "" || user.Password == "")
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Incorrect Email or Password";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Result = null;
                    return BadRequest(response);
                }
                await context.Users.AddAsync(user);
                context.SaveChanges();
                response.IsSuccess = true;
                response.ErrorMessage = null;
                response.StatusCode = HttpStatusCode.Created;
                response.Result = user;

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<Response>> Login(UserDTO _user)
        {
            try
            {
                User? user = await context.Users.FirstOrDefaultAsync(context => context.Email == _user.Email && context.Password == _user.Password);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Incorrect Email or Password";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Result = null;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.ErrorMessage = null;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = user;

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(response);
        }
        [HttpGet("Posts")]
        public async Task<ActionResult<Response>> GetPosts()
        {
            try
            {
                List<Post> Posts = await context.Posts.ToListAsync();
                if (Posts == null)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "No Posts";
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Result = null;
                    return NotFound(response);
                }
                response.IsSuccess = true;
                response.ErrorMessage = null;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = Posts;

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(response);
        }
        [HttpPost("Posts")]
        public async Task<ActionResult<Response>> AddPost([FromForm]AddPostDTO _post)
        {
            try
            {
                if(_post.User == null)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Credentials Expired";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Result = null;
                    return BadRequest(response);
                }
                Response loginresp = LoginValid(_post.User).Result;
                User Loggined = loginresp.Result as User;
                if(loginresp.IsSuccess == false)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Credentials Expired";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Result = null;
                    return BadRequest(response);
                }
                if (_post.Title.Length <= 0 && _post.Description.Length <= 0 && _post.File.Length <= 0 && _post.File == null)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Incorrect Post Fromat";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Result = null;
                    return BadRequest(response);
                }
                Post post = mapper.Map<Post>(_post);
                post.UserId = Loggined.Id;
                byte[] filebytes = FileManager.FileToImage(_post.File).Result;
                if (filebytes == null)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Incorrect File Format";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Result = null;
                    return BadRequest(response);
                }
                post.Image = filebytes;
                await context.Posts.AddAsync(post);
                context.SaveChanges();
                response.IsSuccess = true;
                response.ErrorMessage = null;
                response.StatusCode = HttpStatusCode.Created;
                response.Result = post;

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(response);
        }
        [HttpPost("Posts/Vote")]
        public async Task<ActionResult<Response>> Vote(Agree agree)
        {
            try
            {
                if (agree.Email == null || agree.Password == null || agree.Email == "" || agree.Password == "")
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Credentials Expired";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Result = null;
                    return BadRequest(response);
                }
                UserDTO user = new();
                user.Email = agree.Email;
                user.Password = agree.Password;
                Response loginresp = LoginValid(user).Result;
                if (loginresp.IsSuccess == false)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Credentials Expired";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Result = null;
                    return BadRequest(response);
                }
                Post post = await context.Posts.FirstOrDefaultAsync(x => x.Id == agree.PostId);
              if(post == null)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Post Not Found";
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Result = null;
                    return NotFound(response);
                }
                if(agree.whatvote == true)
                {
                    post.Trues += 1;
                    context.SaveChanges();
                }
                else
                {
                    post.Falses += 1;
                    context.SaveChanges();
                }
                response.IsSuccess = true;
                response.ErrorMessage = null;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = post;
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(response);
        }
    }

}
