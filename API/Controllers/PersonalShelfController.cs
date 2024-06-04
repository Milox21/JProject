using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalShelfController : BaseApiController<PersonalShelf>
    {
        public PersonalShelfController() :base() { }
    }
}
