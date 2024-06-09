using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileTeaApp.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeaController : BaseApiController<TeaDTO>
    {
        [HttpGet("{id}")]
        public override async Task<ActionResult<TeaDTO>> GetByIdAsync(int id)
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

            var teaDto = new TeaDTO
            {
                Id = item.Id,
                Name = item.Name,
                TeaTypeId = item.TeaTypeId,
                CompanyId = item.CompanyId,
                CountryOrigin = item.CountryOrigin,
                Price = item.Price,
                Size = item.Size,
                TeaType = new TeaTypeDTO
                {
                    Id = item.TeaType.Id,
                    Name = item.TeaType.Name,
                    Description = item.TeaType.Description,
                    Temp = item.TeaType.Temp,
                    BrewingTime = item.TeaType.BrewingTime,
                    AmountPerCup = item.TeaType.AmountPerCup,
                },
                Company = new CompanyDTO
                {
                    Id = item.Company.Id,
                    Name = item.Company.Name,
                    Email = item.Company.Email,
                    PhoneNumber = item.Company.PhoneNumber,
                }
            };

            return Ok(teaDto);
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<TeaDTO>>> GetAllAsync()
        {
            var teas = await _context.Teas
                                     .Include(t => t.TeaType)
                                     .Include(t => t.Company)
                                     .Where(x => x.IsActive == true)
                                     .ToListAsync();

            var teaDtos = teas.Select(item => new TeaDTO
            {
                Id = item.Id,
                Name = item.Name,
                TeaTypeId = item.TeaTypeId,
                CompanyId = item.CompanyId,
                CountryOrigin = item.CountryOrigin,
                Price = item.Price,
                Size = item.Size,
                TeaType = new TeaTypeDTO
                {
                    Id = item.TeaType.Id,
                    Name = item.TeaType.Name,
                    Description = item.TeaType.Description,
                    Temp = item.TeaType.Temp,
                    BrewingTime = item.TeaType.BrewingTime,
                    AmountPerCup = item.TeaType.AmountPerCup,
                },
                Company = new CompanyDTO
                {
                    Id = item.Company.Id,
                    Name = item.Company.Name,
                    Email = item.Company.Email,
                    PhoneNumber = item.Company.PhoneNumber,
                }
            }).ToList();

            return Ok(teaDtos);
        }

        [HttpPost]
        public override async Task<ActionResult> CreateAsync([FromBody] TeaDTO teaDto)
        {
            try
            {
                if (teaDto == null)
                {
                    return BadRequest("Tea entity is null.");
                }

                var teaType = await _context.TeaTypes.FindAsync(teaDto.TeaTypeId);
                if (teaType == null)
                {
                    return BadRequest("Invalid TeaTypeId.");
                }

                var company = await _context.Companies.FindAsync(teaDto.CompanyId);
                if (company == null)
                {
                    return BadRequest("Invalid CompanyId.");
                }

                var entity = new Tea
                {
                    Name = teaDto.Name,
                    TeaTypeId = teaDto.TeaTypeId,
                    CompanyId = teaDto.CompanyId,
                    CountryOrigin = teaDto.CountryOrigin,
                    Price = teaDto.Price,
                    Size = teaDto.Size,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                await _context.Teas.AddAsync(entity);
                await _context.SaveChangesAsync();

                return Ok("Tea created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public override async Task<ActionResult> UpdateAsync(int id, [FromBody] TeaDTO teaDto)
        {
            if (id == 0 || teaDto == null)
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

            item.Name = teaDto.Name;
            item.TeaTypeId = teaDto.TeaTypeId;
            item.CompanyId = teaDto.CompanyId;
            item.CountryOrigin = teaDto.CountryOrigin;
            item.Price = teaDto.Price;
            item.Size = teaDto.Size;
            item.UpdatedAt = DateTime.UtcNow;
            item.IsActive = true;

            _context.Teas.Update(item);
            await _context.SaveChangesAsync();
            return Ok("Tea updated successfully");
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult> DeleteAsync(int id)
        {
            var item = await _context.Teas.FindAsync(id);
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
