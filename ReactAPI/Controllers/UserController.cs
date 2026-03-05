using biznis.Interfaces.Services;
using ClassLibrary1.Entities;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ReactAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;


        //public List<User> UserList { get; set; } = new List<User>();
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpGet(Name = "Get_users")]
        public async Task<List<UserDTO>> Get()
        {
           var userList =  await _userService.GetAllAsync();
            return userList;
        }

        [HttpGet(Name = "Get_user")]
        public async Task<List<UserDTO>> GetUser(Guid publicId)
        {
            var userList = await _userService.GetAllAsync();
            return userList;
        }

        [HttpPost(Name = "Add_user")]
        public async Task<bool> Add(string name, string email, string password)
        {
            var userList = await _userService.CreateAsync(name, email, password);
            return true;
        }


        [HttpDelete(Name = "Delete_users")]
        public async Task<bool> Delete(Guid publicId)
        {
            var userList = await _userService.DeleteAsync(publicId);
            return userList;
        }

        [HttpGet(Name = " Get_by_public_id_user")]
        public async Task<UserEntity> GetByPublicIdAsync(Guid publicId)
        {
            var userList = await _userService.GetByPublicIdAsync(publicId);
            return userList;
        }

        
        [HttpPut(Name = "UpdateUser")]
        public async Task<bool> Update(Guid publicId, string name, string email)
        {
            var userList = await _userService.UpdateAsync(publicId, name, email);
            return userList;
        }

        [HttpGet(Name = "AuthenticateUser")]
        public async Task<UserEntity> Authenticate(string email, string password)
        {
            var userList = await _userService.AuthenticateAsync(email, password);
            return userList;
        }

    }
}
