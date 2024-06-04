using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalBrewingHistoryController : BaseApiController<PersonalBrewingHistory>
    {
        public PersonalBrewingHistoryController() : base() { }

    }
}
