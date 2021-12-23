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

        [Authorize("ALLAdministrator")]
        [HttpGet("secret")]
        public async Task<IActionResult> Secret()
        {
            //var i = new { data = "Secret string from Orders API" };
            //var js = Json(i);
            return Ok("Secret string from Orders API");
            //return Ok("Secret string from Orders API");
        }
    }
}
