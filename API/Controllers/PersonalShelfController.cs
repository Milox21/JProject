using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileTeaApp.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalShelfController : BaseApiController<PersonalShelfDTO>
    {

        public PersonalShelfController() : base() {}

        [HttpGet("{id}")]
        public override async Task<ActionResult<PersonalShelfDTO>> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var item = await _context.PersonalShelves
                                     .Include(ps => ps.Tea)
                                     .Include(ps => ps.Profile)
                                     .FirstOrDefaultAsync(e => e.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            var shelfDto = new PersonalShelfDTO
            {
                Id = item.Id,
                ProfileId = item.ProfileId,
                TeaId = item.TeaId,
                Amount = item.Amount,
                Rating = item.Rating,
                Note = item.Note,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,
                IsActive = item.IsActive,
                Tea = new TeaDTO
                {

                        Id = item.Tea.Id,
                        Name = item.Tea.Name,
                        TeaTypeId = item.Tea.TeaTypeId,
                        CompanyId = item.Tea.CompanyId,
                        CountryOrigin = item.Tea.CountryOrigin,
                        Price = item.Tea.Price,
                        Size = item.Tea.Size
                }
            };

            return Ok(shelfDto);
        }
        [HttpGet("all/{id}")]
        public async Task<ActionResult<IEnumerable<PersonalShelfDTO>>> GetAllAsync(int id)
        {
            var personalShelfItems = await _context.PersonalShelves
                                                   .Include(ps => ps.Tea)
                                                   .Include(ps => ps.Profile)
                                                   .Where(ps => ps.IsActive == true && ps.ProfileId == id)
                                                   .ToListAsync();

            var shelfDtos = personalShelfItems.Select(item => new PersonalShelfDTO
            {
                Id = item.Id,
                ProfileId = item.ProfileId,
                TeaId = item.TeaId,
                Amount = item.Amount,
                Rating = item.Rating,
                Note = item.Note,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,
                IsActive = item.IsActive,
                Tea = new TeaDTO
                {

                        Id = item.Tea.Id,
                        Name = item.Tea.Name,
                        TeaTypeId = item.Tea.TeaTypeId,
                        CompanyId = item.Tea.CompanyId,
                        CountryOrigin = item.Tea.CountryOrigin,
                        Price = item.Tea.Price,
                        Size = item.Tea.Size
                    
                }
            }).ToList();

            return Ok(shelfDtos);
        }

        [HttpPost]
        public override async Task<ActionResult> CreateAsync([FromBody] PersonalShelfDTO shelfDto)
        {
            try
            {
                var entity = new PersonalShelf
                {
                    ProfileId = shelfDto.ProfileId,
                    TeaId = shelfDto.TeaId,
                    Amount = shelfDto.Amount,
                    Rating = shelfDto.Rating,
                    Note = shelfDto.Note,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                var existingShelfItem = await _context.PersonalShelves
                                        .FirstOrDefaultAsync(ps => ps.ProfileId == shelfDto.ProfileId && ps.TeaId == shelfDto.TeaId && ps.IsActive == true);

                string message = "";
                if (existingShelfItem != null)
                {
                    existingShelfItem.Amount += entity.Amount;
                    message = "Amount added to existing shelf item.";
                }
                else
                {
                    await _context.PersonalShelves.AddAsync(entity);
                    message = "Personal shelf item created successfully.";
                }   
                await _context.SaveChangesAsync();

                return Ok(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public override async Task<ActionResult> UpdateAsync(int id, [FromBody] PersonalShelfDTO shelfDto)
        {
            if (id == 0 || shelfDto == null)
            {
                return BadRequest();
            }

            var item = await _context.PersonalShelves
                                     .Include(ps => ps.Tea)
                                     .Include(ps => ps.Profile)
                                     .FirstOrDefaultAsync(e => e.Id == id && e.IsActive == true);

            if (item == null)
            {
                return NotFound();
            }

            item.ProfileId = shelfDto.ProfileId;
            item.TeaId = shelfDto.TeaId;
            item.Amount = shelfDto.Amount;
            item.Rating = shelfDto.Rating;
            item.Note = shelfDto.Note;
            item.UpdatedAt = DateTime.UtcNow;
            item.IsActive = shelfDto.IsActive;

            _context.PersonalShelves.Update(item);
            await _context.SaveChangesAsync();
            return Ok("Personal shelf item updated successfully");
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult> DeleteAsync(int id)
        {
            var item = await _context.PersonalShelves.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            item.DeletedAt = DateTime.UtcNow;
            item.IsActive = false;

            await _context.SaveChangesAsync();
            return Ok("Personal shelf item deleted successfully.");
        }
    }
}
