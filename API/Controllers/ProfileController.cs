using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseApiController<Profile>
    {
        private readonly UserService _userService;
        public ProfileController() : base() 
        {
            _userService = new UserService(_context);
        }


        [HttpPost("login")]
        public async Task<ActionResult<int>> LoginAsync([FromBody] Profile entity)
        {
            var (success, userId) = await _userService.Login(entity.Email, entity.Password);
            if (!success)
            {
                return Unauthorized("Invalid email or password.");
            }
            return Ok(userId);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] Profile entity)
        {
            
            bool success = await _userService.Register(entity);
            if (!success)
            {
                return BadRequest("Registration failed. Email may already be in use.");
            }
            return Ok("Registration successful.");
        }

        

    }
}
