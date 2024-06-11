using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionCodeTypeController : BaseApiController<PromotionCodeType>
    {
        public PromotionCodeTypeController():base() { }
    }
}
