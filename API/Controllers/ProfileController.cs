using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseApiController<Profile>
    {
        public ProfileController() : base() { }


        [HttpPost("login")]
        public async Task<ActionResult<int>> Login([FromBody] Profile entity)
        {
            Profile? profile = await _context.Set<Profile>().FirstOrDefaultAsync(x => x.Email == entity.Email && x.Password == entity.Password);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile.Id);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] Profile entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            await _context.Set<Profile>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return Ok("entity created successfully");
        }
    }
}
