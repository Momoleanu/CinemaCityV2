using Microsoft.AspNetCore.Mvc;
using System;

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
