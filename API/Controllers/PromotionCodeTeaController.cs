using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionCodeTeaController : BaseApiController<PromotionCodeTea>
    {
        public PromotionCodeTeaController() : base() { }
    }
}
