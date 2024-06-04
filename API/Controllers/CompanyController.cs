using API.Models;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : BaseApiController<Company>
    {
        public CompanyController():base() { }

    }
}
