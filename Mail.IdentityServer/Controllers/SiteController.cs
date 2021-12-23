using Microsoft.AspNetCore.Mvc;

namespace Mail.IdentityServer.Controllers
{
    public class SiteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
