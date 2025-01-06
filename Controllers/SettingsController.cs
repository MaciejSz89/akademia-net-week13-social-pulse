using Microsoft.AspNetCore.Mvc;

namespace SocialPulse.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
