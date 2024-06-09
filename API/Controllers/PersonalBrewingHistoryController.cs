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
    }
}
