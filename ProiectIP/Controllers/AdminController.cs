using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http;
using System.Web;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace ProiectIP.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public HttpResponseMessage Login(string username, string password)
        {
            string[] lines = System.IO.File.ReadAllLines("C:\\Users\\Dumitru Andrei\\Source\\Repos\\ProiectIP\\ProiectIP\\Admin\\admin.txt");
            if (lines[0] == username && lines[1] == password)
            {
                Console.WriteLine(lines[0] + lines[1]);
                HttpContext.Response.Cookies.Append("AdminCookie", "true", new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                });


            }
            return View();
            
        }
    }
}
