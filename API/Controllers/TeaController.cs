using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeaController : BaseApiController<Tea>
    {
        public TeaController() { }

        [HttpGet("{id}")]
        public override async Task<ActionResult<Tea>> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            Tea? item = await _context.Teas
                                       .Include(t => t.TeaType)
                                       .Include(t => t.Company)
                                       .FirstOrDefaultAsync(e => e.Id == id && e.IsActive == true);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<Tea>>> GetAllAsync()
        {
            var teas = await _context.Teas
                                     .Include(t => t.TeaType)
                                     .Include(t => t.Company)
                                     .Where(x => x.IsActive == true)
                                     .ToListAsync();
            return Ok(teas);
        }

        [HttpPost]
        public override async Task<ActionResult> CreateAsync([FromBody] Tea entity)
        {
            if (entity == null)
            {
                return BadRequest("Tea entity is null.");
            }

            // Validate TeaTypeId
            var teaType = await _context.TeaTypes.FindAsync(entity.TeaTypeId);
            if (teaType == null)
            {
                return BadRequest("Invalid TeaTypeId.");
            }

            // Validate CompanyId
            var company = await _context.Companies.FindAsync(entity.CompanyId);
            if (company == null)
            {
                return BadRequest("Invalid CompanyId.");
            }

            // Set the navigation properties
            entity.TeaType = teaType;
            entity.Company = company;

            await _context.Teas.AddAsync(entity);
            await _context.SaveChangesAsync();

            return Ok("Tea created successfully.");
        }


        [HttpPut("{id}")]
        public override async Task<ActionResult> UpdateAsync(int id, [FromBody] Tea entity)
        {
            if (id == 0 || entity == null)
            {
                return BadRequest();
            }

            Tea? item = await _context.Teas
                                      .Include(t => t.TeaType)
                                      .Include(t => t.Company)
                                      .FirstOrDefaultAsync(e => e.Id == id && e.IsActive == true);

            if (item == null)
            {
                return NotFound();
            }

            _context.Entry(item).CurrentValues.SetValues(entity);
            item.UpdatedAt = DateTime.Now;

            _context.Teas.Update(item);
            await _context.SaveChangesAsync();
            return Ok("Tea updated successfully");
        }
    }
}
