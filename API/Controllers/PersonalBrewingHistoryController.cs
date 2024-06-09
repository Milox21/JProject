using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileTeaApp.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalBrewingHistoryController : BaseApiController<PersonalBrewingHistoryDTO>
    {
        public PersonalBrewingHistoryController() : base() { }

        [HttpPost]
        public override async Task<ActionResult> CreateAsync([FromBody] PersonalBrewingHistoryDTO brewinghistory)
        {
            if (brewinghistory == null)
            {
                return BadRequest("Brewing history entity is null.");
            }

            var entity = new PersonalBrewingHistory
            {
                PersonalShelfId = brewinghistory.PersonalShelfId,
                Temp = brewinghistory.Temp,
                BrewingTime = brewinghistory.BrewingTime,
                Amount = brewinghistory.Amount,
                Note = brewinghistory.Note,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var personalShelf = _context.PersonalShelves.Where(ps => ps.Id == brewinghistory.PersonalShelfId && ps.IsActive == true).FirstOrDefault();

            personalShelf.UpdatedAt = DateTime.UtcNow;

            if(personalShelf.Amount > brewinghistory.Amount)
            {
                personalShelf.Amount -= brewinghistory.Amount;
                await _context.PersonalBrewingHistories.AddAsync(entity);
                await _context.SaveChangesAsync();
                return Ok("Brewing history created successfully.");
            }
            else
            {
                return Ok("Not enough tea in shelf.");
            }
        }

        [HttpGet("PersonalHistory/{id}")]
        public async Task<ActionResult> GetAllByIdAsync(int id)
        {
            if (id == null)
            {
                return BadRequest("id is null.");
            }

            var personalBrewingHistories = await _context.PersonalBrewingHistories
                .Include(pbh => pbh.PersonalShelf)
                .Include(pbh => pbh.PersonalShelf.Tea)
                .Where(pbh => pbh.PersonalShelf.ProfileId == id && pbh.IsActive == true)
                .ToListAsync();

            List<PersonalBrewingHistoryDTO> personalBrewingHistoriesDTO = personalBrewingHistories.Select(pbh => new PersonalBrewingHistoryDTO
            {
                Id = pbh.Id,
                PersonalShelfId = pbh.PersonalShelfId,
                Temp = pbh.Temp,
                BrewingTime = pbh.BrewingTime,
                Amount = pbh.Amount,
                Note = pbh.Note,
                CreatedAt = pbh.CreatedAt,
                UpdatedAt = pbh.UpdatedAt,
                DeletedAt = pbh.DeletedAt,
                IsActive = pbh.IsActive,
                Tea = new TeaDTO
                {
                    Id = pbh.PersonalShelf.Tea.Id,
                    Name = pbh.PersonalShelf.Tea.Name,
                    TeaTypeId = pbh.PersonalShelf.Tea.TeaTypeId,
                    CompanyId = pbh.PersonalShelf.Tea.CompanyId,
                    CountryOrigin = pbh.PersonalShelf.Tea.CountryOrigin,
                    Price = pbh.PersonalShelf.Tea.Price,
                    Size = pbh.PersonalShelf.Tea.Size
                }
            }).ToList();

            List<PersonalBrewingHistoryDTO> sortedPersonalBrewingHistoriesDTO = personalBrewingHistoriesDTO.OrderByDescending(pbh => pbh.CreatedAt).ToList();
            return Ok(sortedPersonalBrewingHistoriesDTO);
        }
    }
}
