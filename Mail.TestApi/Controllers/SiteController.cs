using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mail.WebApi.Controllers
{
    [Route("site/")]
    [ApiController]
    public class SiteController: Controller
    {
        public SiteController()
        {

        }

        [Authorize]
        [HttpGet("secret")]
        public async Task<IActionResult> Secret()
        {
            return Ok("Secret string from Orders API");
        }
    }
}
