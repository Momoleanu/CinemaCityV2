using Microsoft.AspNetCore.Mvc;

namespace ProiectIP.Controllers
{
    public class HelpPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
