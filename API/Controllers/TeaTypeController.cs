using API.Models;
using API.Models.Contexts;
using API.Models.ModelInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileTeaApp.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeaTypeController : BaseApiController<TeaTypeDTO>
    {
        public TeaTypeController() : base()
        {
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<TeaTypeDTO>> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var item = await _context.TeaTypes
                                     .Include(t => t.Teas)
                                     .FirstOrDefaultAsync(e => e.Id == id && e.IsActive == true);

            if (item == null)
            {
                return NotFound();
            }

            var teaTypeDto = new TeaTypeDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Temp = item.Temp,
                BrewingTime = item.BrewingTime,
                AmountPerCup = item.AmountPerCup,
                Teas = item.Teas?.Select(t => new TeaDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    TeaTypeId = t.TeaTypeId,
                    CompanyId = t.CompanyId,
                    CountryOrigin = t.CountryOrigin,
                    Price = t.Price,
                    Size = t.Size
                }).ToList()
            };

            return Ok(teaTypeDto);
        }
        [HttpGet("all")]
        public async Task<List<TeaTypeDTO>> GetTeaTypesAsync()
        {
            var teaTypes = await _context.TeaTypes
                                .Include(t => t.Teas)
                                    .ThenInclude(t => t.Company)
                                .Include(t => t.Teas)
                                    .ThenInclude(t => t.PromotionCodeTeas)
                                .Where(x => x.IsActive == true)
                                .ToListAsync();

            var teaTypeDtos = teaTypes.Select(item => new TeaTypeDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Temp = item.Temp,
                BrewingTime = item.BrewingTime,
                AmountPerCup = item.AmountPerCup,
                Teas = item.Teas?.Select(t => new TeaDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    TeaTypeId = t.TeaTypeId,
                    CompanyId = t.CompanyId,
                    CountryOrigin = t.CountryOrigin,
                    Price = t.Price,
                    Size = t.Size,
                    Company = new CompanyDTO
                    {
                        Id = t.Company.Id,
                        Name = t.Company.Name,
                    },
                    PromotionCodeTea = t.PromotionCodeTeas
                        .Where(p => p.CompanyId == t.CompanyId && p.PromoStart <= DateTime.Today && p.PromoEnd >= DateTime.Today)
                        .OrderByDescending(p => p.PromoAmount)
                        .Select(p => new PromotionCodeTeaDTO
                        {
                            Id = p.Id,
                            TeaId = p.TeaId,
                            CompanyId = p.CompanyId,
                            PromoCode = p.PromoCode,
                            PromoAmount = p.PromoAmount,
                            PromoStart = p.PromoStart,
                            PromoEnd = p.PromoEnd,
                            IsActive = p.IsActive
                        })
                        .FirstOrDefault()
                }).ToList()
            }).ToList();

            return teaTypeDtos;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<TeaTypeDTO>>> GetAllAsync()
        {
            var teaTypes = await _context.TeaTypes
                                         .Include(t => t.Teas)
                                         .Where(x => x.IsActive == true)
                                         .ToListAsync();

            var teaTypeDtos = teaTypes.Select(item => new TeaTypeDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Temp = item.Temp,
                BrewingTime = item.BrewingTime,
                AmountPerCup = item.AmountPerCup,
                Teas = item.Teas?.Select(t => new TeaDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    TeaTypeId = t.TeaTypeId,
                    CompanyId = t.CompanyId,
                    CountryOrigin = t.CountryOrigin,
                    Price = t.Price,
                    Size = t.Size
                }).ToList()
            }).ToList();

            return Ok(teaTypeDtos);
        }

        [HttpPost]
        public override async Task<ActionResult> CreateAsync([FromBody] TeaTypeDTO teaTypeDto)
        {
            if (teaTypeDto == null)
            {
                return BadRequest("TeaType entity is null.");
            }

            var entity = new TeaType
            {
                Name = teaTypeDto.Name,
                Description = teaTypeDto.Description,
                Temp = teaTypeDto.Temp,
                BrewingTime = teaTypeDto.BrewingTime,
                AmountPerCup = teaTypeDto.AmountPerCup,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _context.TeaTypes.AddAsync(entity);
            await _context.SaveChangesAsync();

            return Ok("TeaType created successfully.");
        }

        [HttpPut("{id}")]
        public override async Task<ActionResult> UpdateAsync(int id, [FromBody] TeaTypeDTO teaTypeDto)
        {
            if (id == 0 || teaTypeDto == null)
            {
                return BadRequest();
            }

            var item = await _context.TeaTypes
                                     .Include(t => t.Teas)
                                     .FirstOrDefaultAsync(e => e.Id == id && e.IsActive == true);

            if (item == null)
            {
                return NotFound();
            }

            item.Name = teaTypeDto.Name;
            item.Description = teaTypeDto.Description;
            item.Temp = teaTypeDto.Temp;
            item.BrewingTime = teaTypeDto.BrewingTime;
            item.AmountPerCup = teaTypeDto.AmountPerCup;
            item.UpdatedAt = DateTime.UtcNow;
            item.IsActive = teaTypeDto.IsActive;

            _context.TeaTypes.Update(item);
            await _context.SaveChangesAsync();
            return Ok("TeaType updated successfully");
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult> DeleteAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var entity = await _context.TeaTypes.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok("TeaType deleted successfully");
        }
    }
}
