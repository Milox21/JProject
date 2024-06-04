using API.Models;
using API.Models.Contexts;
using API.Models.ModelInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController<T> : ControllerBase where T : class, IBaseModel
    {
        public readonly DatabaseContext _context;
        public BaseApiController()
        {
            _context = new();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            T? item = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && e.IsActive == true);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAllAsync()
        {
            List<T> entities = await _context.Set<T>()
                                                .Where(x => x.IsActive == true)
                                                .ToListAsync();
            return Ok(entities);
        }

        [HttpPost]
        public virtual async Task<ActionResult> CreateAsync([FromBody] T entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return Ok("entity created successfully");
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> UpdateAsync(int id, [FromBody] T entity)
        {
            if (id == 0 || entity == null)
            {
                return BadRequest();
            }

            T? item = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && e.IsActive == true);

            if (item == null)
            {
                return NotFound();
            }

            _context.Entry(item).CurrentValues.SetValues(entity);
            item.UpdatedAt = DateTime.Now;

            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
            return Ok("entity updated successfully");
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> DeleteAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            T? entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok("entity deleted successfully");
        }
    }
}
